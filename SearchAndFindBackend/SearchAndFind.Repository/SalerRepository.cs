using SearchAndFind.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAndFind.Entities;
using SearchAndFind.DataAccess;
using System.Globalization;
using log4net;

namespace SearchAndFind.Repository
{
    public class SalerRepository : AbstractRepository<Saler>,  ISalerRepository
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public User GetUserByMail(string userMail)
        {

            using (var db = new SearchAndFindDbContext())
                try
                {
                    var queryResult = from s in db.Salers where s.MailAddress.Equals(userMail) select s;
                    return queryResult.Single();
                }
                catch (Exception e)
                {
                    logger.Error("Error getting saler by mail: ", e);
                    throw new RepositoryException("Error al obtener vendedor por Mail");
                }
        }
        public User GetUserByCurrentToken(string currentToken)
        {
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var queryResult = from s in db.Salers where s.CurrentToken.Equals(currentToken) && s.Eliminated.Equals(false) select s;
                    return queryResult.Single();
                }
                catch (Exception e)
                {
                    logger.Error("Error getting saler by token: ", e);
                    throw new RepositoryException("Error al obtener vendedor");
                }
        }

        public ICollection<SalerAvailableForTender> GetSalerNearCardinalCoord(SalerAvailablesToTenderQueryFilter filter)
        {
            using (var context = new SearchAndFindDbContext())
            {
                try {
                    string query = BuildSQLQUery(filter);
                   return  context.Database.SqlQuery<SalerAvailableForTender>(query).ToList();
                }
                catch(Exception e)
                {
                    logger.Error("Error getting saler for query: ", e);
                    throw new RepositoryException("Error al obtener locales cercanos");
                }
            }
                
        }

        private string BuildSQLQUery(SalerAvailablesToTenderQueryFilter filter)
        {
            NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
            numberFormatInfo.NumberDecimalSeparator = ".";
            StringBuilder builder = new StringBuilder();
            builder.Append("select resultQuery.*, resultQuery.distance  from (SELECT s.Id,s.Latitude,s.Length, s.Name,s.ShopName,s.DeviceId, (").Append(filter.EarthRadius).
            Append(" * ACOS(SIN(RADIANS(s.latitude)) * SIN(RADIANS(").Append(filter.QueryLatitude.ToString(numberFormatInfo)).
            Append("))+ COS(RADIANS(s.length - ").Append(filter.QueryLength.ToString(numberFormatInfo)).Append("))* COS(RADIANS(s.latitude)) * COS(RADIANS(").
            Append(filter.QueryLatitude.ToString(numberFormatInfo)).Append(")))) AS distance FROM users s Inner join (select userId from userCategories where CONVERT(uniqueidentifier, categoryId)=CONVERT(uniqueidentifier, '").
            Append(filter.CategoryId).Append("'))sc on CONVERT(uniqueidentifier, s.id)=CONVERT(uniqueidentifier, sc.userId) WHERE (s.discriminator='Saler' AND s.eliminated=0 AND s.latitude BETWEEN ").
            Append(filter.MinLatitude.ToString(numberFormatInfo)).Append(" AND ").Append(filter.MaxLatitude.ToString(numberFormatInfo)).Append(") AND (s.length BETWEEN ").
            Append(filter.MinLength.ToString(numberFormatInfo)).Append(" AND ").Append(filter.MaxLength.ToString(numberFormatInfo)).Append(") And s.shopHourOpen <= ").
            Append(filter.HourQuery).Append(" AND s.shopHourClose>").Append(filter.HourQuery).
            Append(" AND s.shopDaysOpen like '%").Append(filter.DayOfQuery).Append("%') resultQuery where resultQuery.distance<").
            Append(filter.Distance.ToString(numberFormatInfo)).Append(" ORDER BY resultQuery.distance ASC ");
            return builder.ToString();
        }

        public void AddCategoryOnUser(Guid idSaler, Category currentCategory)
        {
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var category = (from c in db.Categories
                                    select c).FirstOrDefault(c => c.Id.Equals(currentCategory.Id));
                    var saler = (from p in db.Salers
                                  select p).FirstOrDefault(p => p.Id.Equals(idSaler));
                    saler.Categories.Add(category);
                    db.Salers.Attach(saler);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    logger.Error("Error adding category to saler: ", e);
                    throw new RepositoryException("Error al agregar categoria a usuario", e);
                }
        }

        public void RemoveCategoryFromUser(Guid idSaler, Category currentCategory)
        {

            using (var db = new SearchAndFindDbContext())
                try
                {
                    var category = (from c in db.Categories
                                    select c).FirstOrDefault(c => c.Id.Equals(currentCategory.Id));
                    var saler = (from p in db.Salers
                                  select p).FirstOrDefault(p => p.Id.Equals(idSaler));
                    saler.Categories.Remove(category);
                    db.Salers.Attach(saler);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    logger.Error("Error removing category from : ", e);
                    throw new RepositoryException("Error al remover categoría de usuario", e);
                }
        }
        public Saler GetSalerWithCategories(Guid salerId)
        {
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var queryResult = from s in db.Salers.Include("Categories") where s.Id.Equals(salerId) && s.Eliminated.Equals(false) select s;
                    return queryResult.Single();
                }
                catch (Exception e)
                {
                    logger.Error("Error getting saler: ", e);
                    throw new RepositoryException("Error al obtener vendedor");
                }
        }
    }
}
