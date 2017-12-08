using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SearchAndFind.Core;
using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services.Test
{
    [TestClass]
    public class QueryServiceTest
    {
        [TestMethod]
        public void TestDoQuery()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            salerManager.Setup(manager => manager.GetSalersNearQueryLocalization(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<DateTime>(), It.IsAny<Guid>())).Returns(salers);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.DoQuery(It.IsAny<QueryRequest>())).Returns(queryDTOResult);
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Latitude = 1, Length = 1, Category = "guid" };
            Response response = service.DoQuery(request);
            Assert.IsTrue(response.Success);
        }

      

        [TestMethod]
        public void TestDoQuery4()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            salerManager.Setup(manager => manager.GetSalersNearQueryLocalization(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<DateTime>(), It.IsAny<Guid>())).Returns(salers);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.DoQuery(It.IsAny<QueryRequest>())).Returns(queryDTOResult);
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Latitude = 1, Length = 1 };
            Response response = service.DoQuery(request);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void TestDoQuery5()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(new UserDTO());
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(new UserDTO());
            salerManager.Setup(manager => manager.GetSalersNearQueryLocalization(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<DateTime>(), It.IsAny<Guid>())).Returns(salers);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.DoQuery(It.IsAny<QueryRequest>())).Returns(queryDTOResult);
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Latitude = 1, Length = 1, Category = "guid" };
            Response response = service.DoQuery(request);
            Assert.IsTrue(response.AuthenticationError);
        }


        [TestMethod]
        public void TestGetPendingQuery()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.GetCurrentQuery(It.IsAny<QueryRequest>())).Returns(queryDTOResult);
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Latitude = 1, Length = 1, Category = "guid" };
            Response response = service.GetPendingQuery(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestGetPendingQuery2()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.GetCurrentQuery(It.IsAny<QueryRequest>())).Returns(queryDTOResult);
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Length = 1, Category = "guid" };
            Response response = service.GetPendingQuery(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestGetPendingQuery3()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.GetCurrentQuery(It.IsAny<QueryRequest>())).Returns(queryDTOResult);
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Latitude = 1, Category = "guid" };
            Response response = service.GetPendingQuery(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestGetPendingQuery4()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.GetCurrentQuery(It.IsAny<QueryRequest>())).Returns(queryDTOResult);
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Latitude = 1, Length = 1 };
            Response response = service.GetPendingQuery(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestGetPendingQuery5()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(new UserDTO());
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(new UserDTO());
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.GetCurrentQuery(It.IsAny<QueryRequest>())).Returns(queryDTOResult);
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Latitude = 1, Length = 1, Category = "guid" };
            Response response = service.GetPendingQuery(request);
            Assert.IsTrue(response.AuthenticationError);
        }

        [TestMethod]
        public void TestCancelQuery()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.CancelQuery(It.IsAny<QueryRequest>()));
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Latitude = 1, Length = 1, Category = "guid" };
            Response response = service.CancelQuery(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestCancelQuery2()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.CancelQuery(It.IsAny<QueryRequest>()));
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Length = 1, Category = "guid" };
            Response response = service.CancelQuery(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestCancelQuery3()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.CancelQuery(It.IsAny<QueryRequest>()));
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Latitude = 1, Category = "guid" };
            Response response = service.CancelQuery(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestCancelQuery4()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.CancelQuery(It.IsAny<QueryRequest>()));
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Latitude = 1, Length = 1 };
            Response response = service.CancelQuery(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestCancelQuery5()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(new UserDTO() );
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.CancelQuery(It.IsAny<QueryRequest>()));
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Latitude = 1, Length = 1, Category = "guid" };
            Response response = service.CancelQuery(request);
            Assert.IsTrue(response.AuthenticationError);
        }


        [TestMethod]
        public void TestConfirmQuery()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            tenderManager.Setup(manager => manager.GetTenderById(It.IsAny<string>())).Returns(new TenderResponse() { SalerDTO = new SalerDTO() });
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.DoQuery(It.IsAny<QueryRequest>())).Returns(queryDTOResult);
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", TenderConfirmId = "tender" };
            Response response = service.ConfirmQuery(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestConfirmQuery2()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            tenderManager.Setup(manager => manager.GetTenderById(It.IsAny<string>())).Returns(new TenderResponse());
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.DoQuery(It.IsAny<QueryRequest>())).Returns(queryDTOResult);
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf" };
            Response response = service.ConfirmQuery(request);
            Assert.IsFalse(response.Success);
        }



        [TestMethod]
        public void TestConfirmQuery3()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var absSalerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var salerManager = new Mock<ISalerManager>();
            var queryManager = new Mock<IQueryManager>();
            var tenderManager = new Mock<ITenderManager>();
            ICollection<SalerAvailableForTenderDTO> salers = new List<SalerAvailableForTenderDTO>();
            salers.Add(new SalerAvailableForTenderDTO());
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(new UserDTO());
            absSalerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(new UserDTO());
            tenderManager.Setup(manager => manager.GetTenderById(It.IsAny<string>())).Returns(new TenderResponse());
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            QueryDTO queryDTOResult = new QueryDTO();
            queryManager.Setup(manager => manager.DoQuery(It.IsAny<QueryRequest>())).Returns(queryDTOResult);
            QueryService service = new QueryService(queryManager.Object, cloudMessageSender.Object, salerManager.Object, absSalerManager.Object, clientManager.Object, tenderManager.Object);
            QueryRequest request = new QueryRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Latitude = 1, Length = 1, Category = "guid" };
            Response response = service.ConfirmQuery(request);
            Assert.IsTrue(response.AuthenticationError);
        }
    }
}
