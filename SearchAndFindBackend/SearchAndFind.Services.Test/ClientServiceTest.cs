using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchAndFind.Core;
using SearchAndFind.DTO;
using Moq;


namespace SearchAndFind.Services.Test
{
    [TestClass]
    public class ClientServiceTest
    {
        [TestMethod]
        public void TestClientServiceSignIn()
        {
            var clientManager= new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(cManager => cManager.SignIn(It.IsAny<ClientRequest>())).Returns(new Response());
            ClientRequest request = new ClientRequest() {  Mail = "test@mail", Password = "123456", AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object,  reviewManager.Object, tenderManager.Object,
                 queryManager.Object);
            Response response=service.SignIn(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestClientServiceSignIn2()
        {
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(cManager => cManager.SignIn(new ClientRequest())).Returns(new Response());
            ClientRequest request = new ClientRequest() {Password = "1234", AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object,  reviewManager.Object, tenderManager.Object,
                queryManager.Object);
            Response response=service.SignIn(request);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        
        public void TestClientServiceSignIn3()
        {
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(cManager => cManager.SignIn(new ClientRequest())).Returns(new Response());
            ClientRequest request = new ClientRequest() { Mail = "test@test", AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object,  reviewManager.Object, tenderManager.Object,
                 queryManager.Object);
            Response response = service.SignIn(request);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void TestClientServiceSignIn4()
        {
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(cManager => cManager.SignIn(new ClientRequest())).Returns(new Response());
            ClientRequest request = new ClientRequest() { Mail = "test", AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object,  reviewManager.Object, tenderManager.Object,
                queryManager.Object);
            Response response = service.SignIn(request);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void TestClientServiceSignIn5()
        {
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(cManager => cManager.SignIn(new ClientRequest())).Returns(new Response());
            ClientRequest request = new ClientRequest() { Mail = "test@test", Password="1", AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object,  reviewManager.Object, tenderManager.Object, queryManager.Object);
            Response response = service.SignIn(request);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void TestClientServiceSignUp()
        {
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(cManager => cManager.SignUp(It.IsAny<ClientRequest>())).Returns(new Response());
            ClientRequest request = new ClientRequest() { Mail = "test@mail", Password = "123456", Name="Test", LastName="Test", AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object,  reviewManager.Object, tenderManager.Object,
                 queryManager.Object);
            Response response=service.SignUp(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestClientServiceSignUp2()
        {
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(cManager => cManager.SignUp(new ClientRequest())).Returns(new Response());
            ClientRequest request = new ClientRequest() { Password = "123456", Name = "Test", LastName = "Test", AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object,  reviewManager.Object, tenderManager.Object,
                queryManager.Object);
            Response response = service.SignUp(request);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void TestClientServiceSignUp3()
        {
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(cManager => cManager.SignUp(new ClientRequest())).Returns(new Response());
            ClientRequest request = new ClientRequest() { Mail = "test@mail",  Name = "Test", LastName = "Test", AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object,  reviewManager.Object, tenderManager.Object,
                queryManager.Object);
            Response response = service.SignUp(request);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void TestClientServiceSignUp4()
        {
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(cManager => cManager.SignUp(It.IsAny<ClientRequest>())).Returns(new Response());
            ClientRequest request = new ClientRequest() { Mail = "test@mail", Password = "123456",  LastName = "Test", AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object,  reviewManager.Object, tenderManager.Object,
                 queryManager.Object);
            Response response = service.SignUp(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestClientServiceSignUp5()
        {
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(cManager => cManager.SignUp(It.IsAny<ClientRequest>())).Returns(new Response());
            ClientRequest request = new ClientRequest() {  Mail = "test@mail", Password = "123456", Name = "Test", AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object,  reviewManager.Object, tenderManager.Object,
                 queryManager.Object);
            Response response = service.SignUp(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestClientServiceSignUp6()
        {
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(cManager => cManager.SignIn(new ClientRequest())).Returns(new Response());
            ClientRequest request = new ClientRequest() { Mail = "test@mail", Password = "126", Name = "Test", LastName = "Test", AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object,  reviewManager.Object, tenderManager.Object,
                queryManager.Object);
            Response response = service.SignUp(request);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void TestClientServiceSignUp7()
        {
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(cManager => cManager.SignIn(new ClientRequest())).Returns(new Response());
            ClientRequest request = new ClientRequest() { Mail = "mail", Password = "123456", Name = "Test", LastName = "Test", AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object,  reviewManager.Object, tenderManager.Object,
                queryManager.Object);
            Response response = service.SignUp(request);
            Assert.IsFalse(response.Success);
        }
        [TestMethod]
        public void TestGetUserById()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            abstractUserManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            clientManager.Setup(manager => manager.GetUserById(It.IsAny<ClientRequest>())).Returns(new ClientResponse());
            ClientRequest request = new ClientRequest() { UserId = userId.ToString(), CurrentToken = userResult.CurrentToken, AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object, reviewManager.Object, tenderManager.Object,
                queryManager.Object);
            Response response = service.GetUserById(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestGetUserById2()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            abstractUserManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            ClientRequest request = new ClientRequest() {  CurrentToken = userResult.CurrentToken, AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object,reviewManager.Object, tenderManager.Object,
                queryManager.Object);
            Response response = service.GetUserById(request);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void TestGetUserById3()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            abstractUserManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            ClientRequest request = new ClientRequest() { UserId = "111", CurrentToken = "12", AuthenticationType = "syf" };
            ClientService service = new ClientService(clientManager.Object, abstractUserManager.Object, reviewManager.Object, tenderManager.Object,
                queryManager.Object);
            Response response = service.GetUserById(request);
            Assert.IsTrue(response.AuthenticationError);
        }
    }
}
