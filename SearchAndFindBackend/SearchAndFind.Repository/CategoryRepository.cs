using SearchAndFind.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAndFind.Entities;
using SearchAndFind.DataAccess;
using log4net;

namespace SearchAndFind.Repository
{
    public class CategoryRepository : AbstractRepository<Category>, ICategoryRepository
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ICollection<Category> GetAll()
        {
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var queryResult = from s in db.Categories select s;
                    return queryResult.OrderBy(cat=>cat.Name).ToList<Category>();
                }
                catch (Exception e)
                {
                    logger.Error("Error getting all categories: ", e);
                    throw new RepositoryException("Error al obtener categorias");
                }
        }
        public Category GetCategoryByName(string categoryName)
        {
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var queryResult = from s in db.Categories where s.Name.Equals(categoryName) select s;
                    return queryResult.Single();
                }
                catch (Exception e)
                {
                    logger.Error("Error getting category by name: ", e);
                    throw new RepositoryException("No se encontró la categoría que busca");
                }
        }

    }
}
