using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAndFind.DTO;
using SearchAndFind.Entities;

namespace SearchAndFind.Core
{
    public class QueryManager : IQueryManager
    {
        private IQueryRepository queryRepository;
        private IUserRepository<Client> clientRepository;
        private ICategoryRepository categoryRepository;
        private IDTOBuilder<QueryDTO, Query> dtoBuilder;
       

        public QueryManager(IQueryRepository repository, IUserRepository<Client> clientRepo, ICategoryRepository categoryRepo, IDTOBuilder<QueryDTO,Query> queryDtoBuilder)
        {
            queryRepository = repository;
            clientRepository = clientRepo;
            categoryRepository = categoryRepo;
            dtoBuilder = queryDtoBuilder;
        }
        public QueryDTO DoQuery(QueryRequest request)
        {
            Category category = categoryRepository.GetCategoryByName(request.Category);
            ValidateCategory(category);
            ValidatePendingQuery(request.UserId);
            Query query = BuildQueryFromRequest(request, category);
            queryRepository.AddObject(query);
            query.Category = category;
            return dtoBuilder.BuildDTO(query);
        }

        private void ValidateCategory(Category category)
        {
            if (category == null)
            {
                throw new ManagerException("Lo sentimos, no contamos con la categoría que busca");
            }
        }

        private Query BuildQueryFromRequest(QueryRequest request,Category category)
        {
            Query query = new Query();
            query.ClientId = Guid.Parse(request.UserId);
            query.CategoryId = category.Id;
            query.Description = request.Descritpion;
            query.ClientLatitude = request.Latitude;
            query.ClientLength = request.Length;
            return query;
        }

        private void ValidatePendingQuery(string userId)
        {
            Query query = queryRepository.GetCurrentQueryByClientId(Guid.Parse(userId));
            if(query!=null && query.State.Equals(Query.PENDING_STATE))
            {
                throw new ManagerException("Existe una consulta en estado pendiente.");
            }
        }

       

        public QueryDTO GetCurrentQuery(QueryRequest request)
        {
            Query queryResult = GetCurrentQueryFromRepository(request.UserId);
            if (queryResult == null)
            {
                throw new ManagerException("Ud no tiene una consulta pendiente");
            }
            return dtoBuilder.BuildDTO(queryResult);
        }

        public void CancelQuery(QueryRequest request)
        {
            UpdateQueryState(request, Query.CANCELED_STATE);
        }

        public void ConfirmQuery(QueryRequest request)
        {
            UpdateQueryState(request, Query.CONFIRMED_STATE);
        }
        private void UpdateQueryState(QueryRequest request, string queryState)
        {
            Query query = GetCurrentQueryFromRepository(request.UserId);
            query.State = queryState;
            queryRepository.UpdateObject(query);
        }

        private Query GetCurrentQueryFromRepository(string userId)
        {
            try { 
                Query query = queryRepository.GetCurrentQueryByClientId(Guid.Parse(userId));
                if (query == null)
                {
                    throw new ManagerException("No se encontró una consulta pendiente para usted");
                }
                return query;
            }
            catch (RepositoryException)
            {
                throw new ManagerException("No se encontró una consulta pendiente para usted");
            }
            
        }

        public QueryDTO GetQueryById(string queryId)
        {
            try
            {
                Query query = queryRepository.GetFullQuery(Guid.Parse(queryId));
                if (query == null)
                {
                    throw new ManagerException("No se encontró  consulta");
                }
                return dtoBuilder.BuildDTO(query) ;
            }
            catch (RepositoryException)
            {
                throw new ManagerException("No se encontró consulta");
            }
        }
    }
}
