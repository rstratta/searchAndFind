using log4net;
using SearchAndFind.Core;
using SearchAndFind.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace SearchAndFind.Repository
{
    public abstract class AbstractRepository<TEntity> : IRepository<TEntity> where TEntity:class
    {

        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        internal DbSet<TEntity> dbSet;
        public void AddObject(TEntity entity)
        {
            using (var db = new SearchAndFindDbContext())
                try
                {
                    dbSet = db.Set<TEntity>();
                    dbSet.Add(entity);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    logger.Error("Error on addEntity: ", e);
                    throw new RepositoryException("Error");
                }


        }
        public void UpdateObject(TEntity entity)
        {
            using (var db = new SearchAndFindDbContext())
                try
                {
                    dbSet = db.Set<TEntity>();
                    dbSet.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified; //verificar que tengamos la pk llamada ID 
                    db.SaveChanges(); 
                }
                catch (Exception e)
                {
                    logger.Error("Error on updateEntity: ", e);
                    throw new RepositoryException("Error");
                }
           

        }

        public virtual TEntity GetById(Guid id)
        {
            TEntity element;
            using (var db = new SearchAndFindDbContext())
                try
                {
                     dbSet = db.Set<TEntity>();
                     element = dbSet.Find(id);
                }
                catch (Exception e)
                {
                    logger.Error("Error on getByIdEntity: ", e);
                    throw new RepositoryException("Error");
                }
            return element;
        }
       

        public void RemoveObject(TEntity entity)
        {
            using (var db = new SearchAndFindDbContext())
            {
                try
                {
                    dbSet = db.Set<TEntity>();
                    if (db.Entry(entity).State == EntityState.Detached)
                    {
                        dbSet.Attach(entity);
                    }
                    dbSet.Remove(entity);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    logger.Error("Error on removeEntity: ", e);
                    throw new RepositoryException("Error");
                }
            }
              
            
        }

       
    }
}
