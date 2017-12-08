using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchAndFind.Core;
using Moq;
using SearchAndFind.DTO;

namespace SearchAndFind.Services.Test
{
  
    [TestClass]
    public class TenderServiceTest
    {
       
        [TestMethod]
        public void TestDoTender()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            clientManager.Setup(manager => manager.GetUserDTOById(It.IsAny<string>())).Returns(userResult);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            tenderManager.Setup(manager => manager.DoTender(It.IsAny<TenderRequest>())).Returns(new TenderDTO() { SalerDTO = new FullSalerDTO() });
            queryManager.Setup(manager => manager.GetQueryById(It.IsAny<string>())).Returns(new QueryDTO());
            TenderService service = new TenderService(salerManager.Object, clientManager.Object, tenderManager.Object, queryManager.Object,  cloudMessageSender.Object);
            TenderRequest request = new TenderRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf",TenderAmount=200 };
            Response response = service.DoTender(request);
            Assert.IsTrue(response.Success);
        }


        [TestMethod]
        public void TestDoTender2()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            clientManager.Setup(manager => manager.GetUserDTOById(It.IsAny<string>())).Returns(userResult);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            tenderManager.Setup(manager => manager.DoTender(It.IsAny<TenderRequest>())).Returns(new TenderDTO() { SalerDTO = new FullSalerDTO() });
            queryManager.Setup(manager => manager.GetQueryById(It.IsAny<string>())).Returns(new QueryDTO());
            TenderService service = new TenderService(salerManager.Object, clientManager.Object, tenderManager.Object, queryManager.Object,  cloudMessageSender.Object);
            TenderRequest request = new TenderRequest() { UserId = userId.ToString(), CurrentToken = "", AuthenticationType = "syf", TenderAmount = 200 };
            Response response = service.DoTender(request);
            Assert.IsTrue(response.AuthenticationError);
        }

        [TestMethod]
        public void TestDoTender3()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            clientManager.Setup(manager => manager.GetUserDTOById(It.IsAny<string>())).Returns(userResult);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            tenderManager.Setup(manager => manager.DoTender(It.IsAny<TenderRequest>())).Returns(new TenderDTO() { SalerDTO = new FullSalerDTO() });
            queryManager.Setup(manager => manager.GetQueryById(It.IsAny<string>())).Returns(new QueryDTO());
            TenderService service = new TenderService(salerManager.Object, clientManager.Object, tenderManager.Object, queryManager.Object,  cloudMessageSender.Object);
            TenderRequest request = new TenderRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf"};
            Response response = service.DoTender(request);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void TestConfirmTender()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            clientManager.Setup(manager => manager.GetUserDTOById(It.IsAny<string>())).Returns(userResult);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            tenderManager.Setup(manager => manager.ConfirmTender(It.IsAny<TenderRequest>())).Returns(new TenderDTO() { SalerDTO = new FullSalerDTO() });
            queryManager.Setup(manager => manager.GetQueryById(It.IsAny<string>())).Returns(new QueryDTO());
            TenderService service = new TenderService(salerManager.Object, clientManager.Object, tenderManager.Object, queryManager.Object,  cloudMessageSender.Object);
            TenderRequest request = new TenderRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf" , TenderId="tenderId"};
            Response response = service.ConfirmTender(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestConfirmTender2()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            clientManager.Setup(manager => manager.GetUserDTOById(It.IsAny<string>())).Returns(userResult);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            tenderManager.Setup(manager => manager.ConfirmTender(It.IsAny<TenderRequest>())).Returns(new TenderDTO() { SalerDTO = new FullSalerDTO() });
            queryManager.Setup(manager => manager.GetQueryById(It.IsAny<string>())).Returns(new QueryDTO());
            TenderService service = new TenderService(salerManager.Object, clientManager.Object, tenderManager.Object, queryManager.Object,  cloudMessageSender.Object);
            TenderRequest request = new TenderRequest() { UserId = userId.ToString(), CurrentToken = "", AuthenticationType = "syf", TenderId = "tenderId" };
            Response response = service.ConfirmTender(request);
            Assert.IsTrue(response.AuthenticationError);
        }

        [TestMethod]
        public void TestConfirmTender3()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            clientManager.Setup(manager => manager.GetUserDTOById(It.IsAny<string>())).Returns(userResult);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            tenderManager.Setup(manager => manager.ConfirmTender(It.IsAny<TenderRequest>())).Returns(new TenderDTO() { SalerDTO = new FullSalerDTO() });
            queryManager.Setup(manager => manager.GetQueryById(It.IsAny<string>())).Returns(new QueryDTO());
            TenderService service = new TenderService(salerManager.Object, clientManager.Object, tenderManager.Object, queryManager.Object,  cloudMessageSender.Object);
            TenderRequest request = new TenderRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf"};
            Response response = service.ConfirmTender(request);
            Assert.IsFalse(response.Success);
        }


        [TestMethod]
        public void TestRevokeTender()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            clientManager.Setup(manager => manager.GetUserDTOById(It.IsAny<string>())).Returns(userResult);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            tenderManager.Setup(manager => manager.RevokeTender(It.IsAny<TenderRequest>())).Returns(new TenderDTO() { SalerDTO = new FullSalerDTO() });
            queryManager.Setup(manager => manager.GetQueryById(It.IsAny<string>())).Returns(new QueryDTO());
            TenderService service = new TenderService(salerManager.Object, clientManager.Object, tenderManager.Object, queryManager.Object,  cloudMessageSender.Object);
            TenderRequest request = new TenderRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", TenderId = "tenderId" };
            Response response = service.RevokeTender(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestRevokeTender2()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            clientManager.Setup(manager => manager.GetUserDTOById(It.IsAny<string>())).Returns(userResult);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            tenderManager.Setup(manager => manager.RevokeTender(It.IsAny<TenderRequest>())).Returns(new TenderDTO() { SalerDTO = new FullSalerDTO() });
            queryManager.Setup(manager => manager.GetQueryById(It.IsAny<string>())).Returns(new QueryDTO());
            TenderService service = new TenderService(salerManager.Object, clientManager.Object, tenderManager.Object, queryManager.Object,  cloudMessageSender.Object);
            TenderRequest request = new TenderRequest() { UserId = userId.ToString(), CurrentToken = "", AuthenticationType = "syf", TenderId = "tenderId" };
            Response response = service.RevokeTender(request);
            Assert.IsTrue(response.AuthenticationError);
        }

        [TestMethod]
        public void TestRevokeTender3()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            clientManager.Setup(manager => manager.GetUserDTOById(It.IsAny<string>())).Returns(userResult);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            tenderManager.Setup(manager => manager.RevokeTender(It.IsAny<TenderRequest>())).Returns(new TenderDTO() { SalerDTO = new FullSalerDTO() });
            queryManager.Setup(manager => manager.GetQueryById(It.IsAny<string>())).Returns(new QueryDTO());
            TenderService service = new TenderService(salerManager.Object, clientManager.Object, tenderManager.Object, queryManager.Object,  cloudMessageSender.Object);
            TenderRequest request = new TenderRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf" };
            Response response = service.RevokeTender(request);
            Assert.IsFalse(response.Success);
        }
    }
}
