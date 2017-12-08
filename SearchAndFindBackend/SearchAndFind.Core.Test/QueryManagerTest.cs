using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchAndFind.Entities;
using Moq;
using SearchAndFind.DTO;

namespace SearchAndFind.Core.Test
{
    [TestClass]
    public class QueryManagerTest
    {
        public QueryManagerTest()
        {

        }

        [TestMethod]
        public void TestDoQuery()
        {
            Query query = null;
            Guid clientGuid = Guid.NewGuid();
            Client client = new Client();
            client.Id = clientGuid;
            var queryRepository = new Mock<IQueryRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository.Setup(repository => repository.GetCategoryByName(It.IsAny<string>())).Returns(new Category("categoryTest", "categoryTestDesc"));
            queryRepository.Setup(repository => repository.AddObject(It.IsAny<Query>()));
            queryRepository.Setup(repository => repository.GetCurrentQueryByClientId(It.IsAny<Guid>())).Returns(query);
            clientRepository.Setup(repository => repository.GetUserByCurrentToken(It.IsAny<string>())).Returns(client);
            IQueryManager queryManager = new QueryManager(queryRepository.Object, clientRepository.Object, categoryRepository.Object, new QueryDTOBuilder());
            QueryRequest request = new QueryRequest() { CurrentToken=Guid.NewGuid().ToString(), UserId= client.Id.ToString(), Category= "categoryTest", Latitude= -34.883762, Length = -56.143848, Descritpion="categoryDescTest" };
            QueryDTO queryDTO = queryManager.DoQuery(request);
            Assert.IsNotNull(queryDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestDoQuery2()
        {
            Query query = null;
            Guid clientGuid = Guid.NewGuid();
            Client client = new Client();
            client.Id = clientGuid;
            Category category = null;
            var queryRepository = new Mock<IQueryRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository.Setup(repository => repository.GetCategoryByName(It.IsAny<string>())).Returns(category);
            queryRepository.Setup(repository => repository.AddObject(It.IsAny<Query>()));
            queryRepository.Setup(repository => repository.GetCurrentQueryByClientId(It.IsAny<Guid>())).Returns(query);
            clientRepository.Setup(repository => repository.GetUserByCurrentToken(It.IsAny<string>())).Returns(client);
            IQueryManager queryManager = new QueryManager(queryRepository.Object, clientRepository.Object, categoryRepository.Object, new QueryDTOBuilder());
            QueryRequest request = new QueryRequest() { CurrentToken = Guid.NewGuid().ToString(), UserId = client.Id.ToString(), Category = "categoryTest", Latitude = -34.883762, Length = -56.143848, Descritpion = "categoryDescTest" };
            queryManager.DoQuery(request);
        }

        

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestDoQuery3()
        {
            Query query = new Query();
            Guid clientGuid = Guid.NewGuid();
            Client client = new Client();
            client.Id = clientGuid;
            var queryRepository = new Mock<IQueryRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository.Setup(repository => repository.GetCategoryByName(It.IsAny<string>())).Returns(new Category("categoryTest", "categoryTestDesc"));
            queryRepository.Setup(repository => repository.AddObject(It.IsAny<Query>()));
            queryRepository.Setup(repository => repository.GetCurrentQueryByClientId(It.IsAny<Guid>())).Returns(query);
            clientRepository.Setup(repository => repository.GetUserByCurrentToken(It.IsAny<string>())).Returns(client);
            IQueryManager queryManager = new QueryManager(queryRepository.Object, clientRepository.Object, categoryRepository.Object, new QueryDTOBuilder());
            QueryRequest request = new QueryRequest() { CurrentToken = Guid.NewGuid().ToString(), UserId = client.Id.ToString(), Category = "categoryTest", Latitude = -34.883762, Length = -56.143848, Descritpion = "categoryDescTest" };
            queryManager.DoQuery(request);
        }

      

        [TestMethod]
        public void TestCancelQuery()
        {
            Query query = new Query();
            Guid clientGuid = Guid.NewGuid();
            Client client = new Client();
            client.Id = clientGuid;
            var queryRepository = new Mock<IQueryRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            queryRepository.Setup(repository => repository.UpdateObject(It.IsAny<Query>()));
            queryRepository.Setup(repository => repository.GetCurrentQueryByClientId(It.IsAny<Guid>())).Returns(query);
            clientRepository.Setup(repository => repository.GetUserByCurrentToken(It.IsAny<string>())).Returns(client);
            IQueryManager queryManager = new QueryManager(queryRepository.Object, clientRepository.Object, categoryRepository.Object, new QueryDTOBuilder());
            QueryRequest request = new QueryRequest() { CurrentToken = Guid.NewGuid().ToString(), UserId = clientGuid.ToString(), Category = "categoryTest", Latitude = -34.883762, Length = -56.143848, Descritpion = "categoryDescTest" };
            queryManager.CancelQuery(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestCancelQuery2()
        {
            Query query = null;
            Guid clientGuid = Guid.NewGuid();
            Client client = new Client();
            client.Id = clientGuid;
            var queryRepository = new Mock<IQueryRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            queryRepository.Setup(repository => repository.UpdateObject(It.IsAny<Query>()));
            queryRepository.Setup(repository => repository.GetCurrentQueryByClientId(It.IsAny<Guid>())).Returns(query);
            clientRepository.Setup(repository => repository.GetUserByCurrentToken(It.IsAny<string>())).Returns(client);
            IQueryManager queryManager = new QueryManager(queryRepository.Object, clientRepository.Object, categoryRepository.Object, new QueryDTOBuilder());
            QueryRequest request = new QueryRequest() { CurrentToken = Guid.NewGuid().ToString(), UserId = clientGuid.ToString(), Category = "categoryTest", Latitude = -34.883762, Length = -56.143848, Descritpion = "categoryDescTest" };
            queryManager.CancelQuery(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestCancelQuery3()
        {
            Query query = null;
            Guid clientGuid = Guid.NewGuid();
            Client client = new Client();
            var queryRepository = new Mock<IQueryRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            queryRepository.Setup(repository => repository.UpdateObject(It.IsAny<Query>()));
            queryRepository.Setup(repository => repository.GetCurrentQueryByClientId(It.IsAny<Guid>())).Returns(query);
            clientRepository.Setup(repository => repository.GetUserByCurrentToken(It.IsAny<string>())).Returns(client);
            IQueryManager queryManager = new QueryManager(queryRepository.Object, clientRepository.Object, categoryRepository.Object, new QueryDTOBuilder());
            QueryRequest request = new QueryRequest() { CurrentToken = Guid.NewGuid().ToString(), UserId = clientGuid.ToString(), Category = "categoryTest", Latitude = -34.883762, Length = -56.143848, Descritpion = "categoryDescTest" };
            queryManager.CancelQuery(request);
        }

        [TestMethod]
        public void TestConfirmQuery()
        {
            Query query = new Query();
            Guid clientGuid = Guid.NewGuid();
            Client client = new Client();
            client.Id = clientGuid;
            var queryRepository = new Mock<IQueryRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            queryRepository.Setup(repository => repository.UpdateObject(It.IsAny<Query>()));
            queryRepository.Setup(repository => repository.GetCurrentQueryByClientId(It.IsAny<Guid>())).Returns(query);
            clientRepository.Setup(repository => repository.GetUserByCurrentToken(It.IsAny<string>())).Returns(client);
            IQueryManager queryManager = new QueryManager(queryRepository.Object, clientRepository.Object, categoryRepository.Object, new QueryDTOBuilder());
            QueryRequest request = new QueryRequest() { CurrentToken = Guid.NewGuid().ToString(), UserId = clientGuid.ToString(), Category = "categoryTest", Latitude = -34.883762, Length = -56.143848, Descritpion = "categoryDescTest" };
            queryManager.ConfirmQuery(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestConfirmQuery2()
        {
            Query query = null;
            Guid clientGuid = Guid.NewGuid();
            Client client = new Client();
            client.Id = clientGuid;
            var queryRepository = new Mock<IQueryRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            queryRepository.Setup(repository => repository.UpdateObject(It.IsAny<Query>()));
            queryRepository.Setup(repository => repository.GetCurrentQueryByClientId(It.IsAny<Guid>())).Returns(query);
            clientRepository.Setup(repository => repository.GetUserByCurrentToken(It.IsAny<string>())).Returns(client);
            IQueryManager queryManager = new QueryManager(queryRepository.Object, clientRepository.Object, categoryRepository.Object, new QueryDTOBuilder());
            QueryRequest request = new QueryRequest() { CurrentToken = Guid.NewGuid().ToString(), UserId = clientGuid.ToString(), Category = "categoryTest", Latitude = -34.883762, Length = -56.143848, Descritpion = "categoryDescTest" };
            queryManager.ConfirmQuery(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestConfirmQuery3()
        {
            Query query = null;
            Guid clientGuid = Guid.NewGuid();
            Client client = new Client();
            var queryRepository = new Mock<IQueryRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            queryRepository.Setup(repository => repository.UpdateObject(It.IsAny<Query>()));
            queryRepository.Setup(repository => repository.GetCurrentQueryByClientId(It.IsAny<Guid>())).Returns(query);
            clientRepository.Setup(repository => repository.GetUserByCurrentToken(It.IsAny<string>())).Returns(client);
            IQueryManager queryManager = new QueryManager(queryRepository.Object, clientRepository.Object, categoryRepository.Object, new QueryDTOBuilder());
            QueryRequest request = new QueryRequest() { CurrentToken = Guid.NewGuid().ToString(), UserId = clientGuid.ToString(), Category = "categoryTest", Latitude = -34.883762, Length = -56.143848, Descritpion = "categoryDescTest" };
            queryManager.ConfirmQuery(request);
        }
        [TestMethod]
        public void TestGetCurrentQuery()
        {
            Query query = new Query();
            query.Category = new Category("CategoryTest", "categoryDesc");
            Guid clientGuid = Guid.NewGuid();
            Client client = new Client();
            client.Id = clientGuid;
            var queryRepository = new Mock<IQueryRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            queryRepository.Setup(repository => repository.UpdateObject(It.IsAny<Query>()));
            queryRepository.Setup(repository => repository.GetCurrentQueryByClientId(It.IsAny<Guid>())).Returns(query);
            clientRepository.Setup(repository => repository.GetUserByCurrentToken(It.IsAny<string>())).Returns(client);
            IQueryManager queryManager = new QueryManager(queryRepository.Object, clientRepository.Object, categoryRepository.Object, new QueryDTOBuilder());
            QueryRequest request = new QueryRequest() { CurrentToken = Guid.NewGuid().ToString(), UserId = clientGuid.ToString(), Category = "categoryTest", Latitude = -34.883762, Length = -56.143848, Descritpion = "categoryDescTest" };
            QueryDTO dto=queryManager.GetCurrentQuery(request);
            Assert.IsNotNull(dto);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestGetCurrentQuery2()
        {
            Query query = null;
            Guid clientGuid = Guid.NewGuid();
            Client client = new Client();
            client.Id = clientGuid;
            var queryRepository = new Mock<IQueryRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            queryRepository.Setup(repository => repository.UpdateObject(It.IsAny<Query>()));
            queryRepository.Setup(repository => repository.GetCurrentQueryByClientId(It.IsAny<Guid>())).Returns(query);
            clientRepository.Setup(repository => repository.GetUserByCurrentToken(It.IsAny<string>())).Returns(client);
            IQueryManager queryManager = new QueryManager(queryRepository.Object, clientRepository.Object, categoryRepository.Object, new QueryDTOBuilder());
            QueryRequest request = new QueryRequest() { CurrentToken = Guid.NewGuid().ToString(), UserId = clientGuid.ToString(), Category = "categoryTest", Latitude = -34.883762, Length = -56.143848, Descritpion = "categoryDescTest" };
            queryManager.GetCurrentQuery(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestGetCurrentQuery3()
        {
            Query query = null;
            Guid clientGuid = Guid.NewGuid();
            Client client = new Client();
            var queryRepository = new Mock<IQueryRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            queryRepository.Setup(repository => repository.UpdateObject(It.IsAny<Query>()));
            queryRepository.Setup(repository => repository.GetCurrentQueryByClientId(It.IsAny<Guid>())).Returns(query);
            clientRepository.Setup(repository => repository.GetUserByCurrentToken(It.IsAny<string>())).Returns(client);
            IQueryManager queryManager = new QueryManager(queryRepository.Object, clientRepository.Object, categoryRepository.Object, new QueryDTOBuilder());
            QueryRequest request = new QueryRequest() { CurrentToken = Guid.NewGuid().ToString(), UserId = clientGuid.ToString(), Category = "categoryTest", Latitude = -34.883762, Length = -56.143848, Descritpion = "categoryDescTest" };
            queryManager.GetCurrentQuery(request);
        }

    }
}