using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchAndFind.Core;
using SearchAndFind.Entities;

namespace SearchAndFind.Repository.Test
{
    
    [TestClass]
    public class QueryRepositoryTest
    {
        private IQueryRepository queryRepository;
        private Guid categoryId;
        private Category category;
        private Guid userId;
        public QueryRepositoryTest()
        {
            userId = Guid.NewGuid();
            categoryId = Guid.NewGuid();
            TestUtils.CleanDatabase();
            queryRepository = new QueryRepository();
            ICategoryRepository categoryRepository = new CategoryRepository();
            category = new Category("CategoryTest", "CategoryDesc");
            category.Id = categoryId;
            categoryRepository.AddObject(category);
            IUserRepository<Client> clientRepository = new ClientRepository();
            Client client = new Client();
            client.Id = userId;
            clientRepository.AddObject(client);
        }

        private Query CreateQueryTest()
        {
            Query query = new Query();
            query.CategoryId = categoryId;
            query.ClientId = userId;
            query.ClientLatitude = -34.883762;
            query.ClientLength = -56.143848;
            query.Description = "test";
            return query;
        }

        [TestMethod]
        public void TestAddQueryRepository()
        {
            Query query = CreateQueryTest();
            queryRepository.AddObject(query);
            Assert.AreEqual(query,queryRepository.GetById(query.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryException))]
        public void TestAddQueryRepository2()
        {
            Query query = CreateQueryTest();
            queryRepository.AddObject(query);
            queryRepository.AddObject(query);
        }

        [TestMethod]
        public void TestUpdateStateQueryRepository()
        {
            Query query = CreateQueryTest();
            queryRepository.AddObject(query);
            query.State = Query.CONFIRMED_STATE;
            queryRepository.UpdateObject(query);
            Assert.AreEqual(Query.CONFIRMED_STATE,queryRepository.GetById(query.Id).State);
        }

        [TestMethod]
        public void TestUpdateRetryQueryRepository()
        {
            var retry = 1;
            Query query = CreateQueryTest();
            queryRepository.AddObject(query);
            query.Retry = retry;
            queryRepository.UpdateObject(query);
            Assert.AreEqual(retry, queryRepository.GetById(query.Id).Retry);
        }

        [TestMethod]
        public void TestGetCurrentQueryRepository()
        {
            Query query = CreateQueryTest();
            queryRepository.AddObject(query);
            Assert.AreEqual(query, queryRepository.GetCurrentQueryByClientId(userId));
        }

        [TestMethod]
        public void TestGetCurrentQueryRepository2()
        {
            Query query = CreateQueryTest();
            queryRepository.AddObject(query);
            query.State = Query.CANCELED_STATE;
            queryRepository.UpdateObject(query);
            Query result=queryRepository.GetCurrentQueryByClientId(userId);
            Assert.IsNull(result);
        }
    }
}
