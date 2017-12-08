using log4net;
using SearchAndFind.Core;
using SearchAndFind.DataAccess;
using SearchAndFind.Entities;
using SearchAndFind.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Repository
{
    public class ClientRepository : AbstractRepository<Client>, IUserRepository<Client>
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public User GetUserByMail(string userMail)
        {
            
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var queryResult = from c in db.Clients where c.MailAddress.Equals(userMail) select c;
                    return queryResult.Single();
                }
                catch (Exception e)
                {
                    logger.Error("Error getting client by mail: ", e);
                    throw new RepositoryException("Error al obtener cliente por Mail");
                }
        }

        public User GetUserByCurrentToken(string currentToken)
        {
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var queryResult = from client in db.Clients where client.CurrentToken.Equals(currentToken) && client.Eliminated.Equals(false) select client;
                    return queryResult.Single();
                }
                catch (Exception e)
                {
                    logger.Error("Error getting client by token: ", e);
                    throw new RepositoryException("Error al obtener client");
                }
        }

        public void AddCategoryOnUser(Guid id, Category currentCategory)
        {
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var category = (from c in db.Categories
                                    select c).FirstOrDefault(c => c.Id.Equals(currentCategory.Id));
                    var client = (from p in db.Clients
                                   select p).FirstOrDefault(p => p.Id.Equals(id));
                    client.Categories.Add(category);
                    db.Clients.Attach(client);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    logger.Error("Error adding category in client: ", e);
                    throw new RepositoryException("Error al agregar categoria a usuario", e);
                }
        }

       public void RemoveCategoryFromUser(Guid id, Category currentCategory)
        {

            using (var db = new SearchAndFindDbContext())
                try
                {
                    var category = (from c in db.Categories
                                    select c).FirstOrDefault(c => c.Id.Equals(currentCategory.Id));
                    var client = (from p in db.Clients
                                   select p).FirstOrDefault(p => p.Id.Equals(id));
                    client.Categories.Remove(category);
                    db.Clients.Attach(client);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    logger.Error("Error removing category from  client : ", e);
                    throw new RepositoryException("Error al remover categoría de usuario", e);
                }
        }
    }
}
