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

    public class QueryRepository : AbstractRepository<Query>, IQueryRepository
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Query GetCurrentQueryByClientId(Guid clientId)
        {
            Query result = null;
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var queryResult = from query in db.Queries where query.ClientId.Equals(clientId) && query.State.Equals(Query.PENDING_STATE) select query;
                    if(queryResult.Count()>0)
                        result = queryResult.Single();
                    return result;
                }
                catch (Exception e)
                {
                    logger.Error("Error getting query from clientId", e);
                    throw new RepositoryException("Error al obtener consulta");
                }
        }
        public Query GetFullQuery(Guid queryId)
        {
            Query result = null;
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var queryResult = from query in db.Queries.Include("Category").Include("Client") where query.Id.Equals(queryId) select query;
                    if (queryResult.Count() > 0)
                        result = queryResult.Single();
                    return result;
                }
                catch (Exception e)
                {
                    logger.Error("Error getting query ", e);
                    throw new RepositoryException("Error al obtener consulta");
                }
        }
    }
}
