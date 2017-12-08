using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SearchAndFind.Core;
using SearchAndFind.DTO;


namespace SearchAndFind.Services.Test
{

        [TestClass]
        public class SalerServiceTest
        {
            public SalerServiceTest()
            {

            }

            [TestMethod]
            public void TestSalerServiceSignIn()
            {
                var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
                var reviewManager = new Mock<IReviewManager>();
                var tenderManager = new Mock<ITenderManager>();
                var salerMan = new Mock<ISalerManager>();
                var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
                var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
                salerManager.Setup(cManager => cManager.SignIn(It.IsAny<SalerRequest>())).Returns(new Response());
                SalerRequest request = new SalerRequest() { Mail = "test@mail", Password = "123456", AuthenticationType = "syf" };
                SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
                    ,  clientManager.Object);
                Response response = service.SignIn(request);
                Assert.IsTrue(response.Success);
            }

            [TestMethod]
            public void TestSalerServiceSignIn2()
            {
                var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
                var reviewManager = new Mock<IReviewManager>();
                var tenderManager = new Mock<ITenderManager>();
                var queryManager = new Mock<IQueryManager>();
                var salerMan = new Mock<ISalerManager>();
                var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
                var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
                salerManager.Setup(cManager => cManager.SignIn(new SalerRequest())).Returns(new Response());
                SalerRequest request = new SalerRequest() { Password = "1234", AuthenticationType = "syf" };
                SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
                    ,  clientManager.Object);
                Response response = service.SignIn(request);
                Assert.IsFalse(response.Success);
            }

            [TestMethod]
            public void TestSalerServiceSignIn3()
            {
                var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
                var reviewManager = new Mock<IReviewManager>();
                var tenderManager = new Mock<ITenderManager>();
                var queryManager = new Mock<IQueryManager>();
                var salerMan = new Mock<ISalerManager>();
                var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
                var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
                salerManager.Setup(cManager => cManager.SignIn(new SalerRequest())).Returns(new Response());
                SalerRequest request = new SalerRequest() { Mail = "test@test", AuthenticationType = "syf" };
                SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
                    ,  clientManager.Object);
                Response response = service.SignIn(request);
                Assert.IsFalse(response.Success);
            }

            [TestMethod]
            public void TestSalerServiceSignIn4()
            {
                var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
                var reviewManager = new Mock<IReviewManager>();
                var tenderManager = new Mock<ITenderManager>();
                var queryManager = new Mock<IQueryManager>();
                var salerMan = new Mock<ISalerManager>();
                var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
                var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
                salerManager.Setup(cManager => cManager.SignIn(new SalerRequest())).Returns(new Response());
                SalerRequest request = new SalerRequest() { Mail = "test", AuthenticationType = "syf" };
                SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
        ,  clientManager.Object);
                Response response = service.SignIn(request);
                Assert.IsFalse(response.Success);
            }

            [TestMethod]
            public void TestSalerServiceSignIn5()
            {
                var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
                var reviewManager = new Mock<IReviewManager>();
                var tenderManager = new Mock<ITenderManager>();
                var queryManager = new Mock<IQueryManager>();
                var salerMan = new Mock<ISalerManager>();
                var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
                var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
                salerManager.Setup(cManager => cManager.SignIn(new SalerRequest())).Returns(new Response());
                SalerRequest request = new SalerRequest() { Mail = "test@test", Password = "1", AuthenticationType = "syf" };
                SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
        ,  clientManager.Object);
                Response response = service.SignIn(request);
                Assert.IsFalse(response.Success);
            }

            [TestMethod]

            public void TestSalerServiceSignUp()
            {
                var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
                var reviewManager = new Mock<IReviewManager>();
                var tenderManager = new Mock<ITenderManager>();
                var queryManager = new Mock<IQueryManager>();
                var salerMan = new Mock<ISalerManager>();
                var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
                var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
                salerManager.Setup(cManager => cManager.SignUp(It.IsAny<SalerRequest>())).Returns(new Response());
                SalerRequest request = new SalerRequest() { Mail = "test@mail", Password = "123456", Name = "Test", LastName = "Test", AuthenticationType = "syf" };
                SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
    ,  clientManager.Object);
                Response response = service.SignUp(request);
                Assert.IsTrue(response.Success);
            }

            [TestMethod]
            public void TestSalerServiceSignUp2()
            {
                var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
                var reviewManager = new Mock<IReviewManager>();
                var tenderManager = new Mock<ITenderManager>();
                var queryManager = new Mock<IQueryManager>();
                var salerMan = new Mock<ISalerManager>();
                var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
                var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
                salerManager.Setup(cManager => cManager.SignUp(new SalerRequest())).Returns(new Response());
                SalerRequest request = new SalerRequest() { Password = "123456", Name = "Test", LastName = "Test", AuthenticationType = "syf" };
                SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
    ,  clientManager.Object);
                Response response = service.SignUp(request);
                Assert.IsFalse(response.Success);
            }

            [TestMethod]
            public void TestSalerServiceSignUp3()
            {
                var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
                var reviewManager = new Mock<IReviewManager>();
                var tenderManager = new Mock<ITenderManager>();
                var queryManager = new Mock<IQueryManager>();
                var salerMan = new Mock<ISalerManager>();
                var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
                var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
                salerManager.Setup(cManager => cManager.SignUp(new SalerRequest())).Returns(new Response());
                SalerRequest request = new SalerRequest() { Mail = "test@mail", Name = "Test", LastName = "Test", AuthenticationType = "syf" };
                SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
    ,  clientManager.Object);
                Response response = service.SignUp(request);
                Assert.IsFalse(response.Success);
            }

            [TestMethod]
            public void TestSalerServiceSignUp4()
            {
                var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
                var reviewManager = new Mock<IReviewManager>();
                var tenderManager = new Mock<ITenderManager>();
                var queryManager = new Mock<IQueryManager>();
                var salerMan = new Mock<ISalerManager>();
                var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
                var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
                salerManager.Setup(cManager => cManager.SignUp(It.IsAny<SalerRequest>())).Returns(new Response());
                SalerRequest request = new SalerRequest() { Mail = "test@mail", Password = "123456", LastName = "Test", AuthenticationType = "syf" };
                SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
    ,  clientManager.Object);
                Response response = service.SignUp(request);
                Assert.IsTrue(response.Success);
            }

            [TestMethod]
            public void TestSalerServiceSignUp5()
            {
                var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
                var reviewManager = new Mock<IReviewManager>();
                var tenderManager = new Mock<ITenderManager>();
                var queryManager = new Mock<IQueryManager>();
                var salerMan = new Mock<ISalerManager>();
                var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
                var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
                salerManager.Setup(cManager => cManager.SignUp(It.IsAny<SalerRequest>())).Returns(new Response());
                SalerRequest request = new SalerRequest() { Mail = "test@mail", Password = "123456", Name = "Test", AuthenticationType = "syf" };
                SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
    ,  clientManager.Object);
                Response response = service.SignUp(request);
                Assert.IsTrue(response.Success);
            }

            [TestMethod]
            public void TestSalerServiceSignIn6()
            {
                var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
                var reviewManager = new Mock<IReviewManager>();
                var tenderManager = new Mock<ITenderManager>();
                var queryManager = new Mock<IQueryManager>();
                var salerMan = new Mock<ISalerManager>();
                var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
                var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
                salerManager.Setup(cManager => cManager.SignIn(new SalerRequest())).Returns(new Response());
                SalerRequest request = new SalerRequest() { Mail = "test@mail", Password = "126", Name = "Test", LastName = "Test", AuthenticationType = "syf" };
                SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
    ,  clientManager.Object);
                Response response = service.SignIn(request);
                Assert.IsFalse(response.Success);
            }

            [TestMethod]
            public void TestSalerServiceSignUp7()
            {
                var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
                var reviewManager = new Mock<IReviewManager>();
                var tenderManager = new Mock<ITenderManager>();
                var queryManager = new Mock<IQueryManager>();
                var salerMan = new Mock<ISalerManager>();
                var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
                var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
                salerManager.Setup(cManager => cManager.SignIn(new SalerRequest())).Returns(new Response());
                SalerRequest request = new SalerRequest() { Mail = "mail", Password = "123456", Name = "Test", LastName = "Test", AuthenticationType = "syf" };
                SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
    ,  clientManager.Object);
                Response response = service.SignIn(request);
                Assert.IsFalse(response.Success);
            }
        [TestMethod]
        public void TestUpdateAccountSuccess()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var salerMan = new Mock<ISalerManager>();
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var abstractUserManager = new Mock<AbstractUserManager<SalerRequest>>();
            var authenticationController = new Mock<AuthenticationController>();
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            salerManager.Setup(manager => manager.GetUserById(It.IsAny<SalerRequest>())).Returns(new SalerResponse());
            salerManager.Setup(manager => manager.UpdateAccount(It.IsAny<SalerRequest>())).Returns(new SalerResponse());
            SalerRequest request = new SalerRequest() { UserId=userResult.Id.ToString(),Mail = "mail", CurrentToken = currentToken.ToString(), Password = "123456", DeviceId="1234562",Name = "Test", LastName = "Test", AuthenticationType = "syf" };
            SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
, clientManager.Object);
            Response response = service.UpdateAccount(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestGetUserById()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var salerMan = new Mock<ISalerManager>();
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            salerManager.Setup(manager => manager.GetUserById(It.IsAny<SalerRequest>())).Returns(new SalerResponse());
            SalerRequest request = new SalerRequest() { UserId = userId.ToString(), CurrentToken=userResult.CurrentToken, AuthenticationType = "syf" };
            SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
, clientManager.Object);
            Response response = service.GetUserById(request);
            Assert.IsTrue(response.Success);


        }

        [TestMethod]
        public void TestGetUserById2()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var salerMan = new Mock<ISalerManager>();
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            SalerRequest request = new SalerRequest() {  CurrentToken = userResult.CurrentToken, AuthenticationType = "syf" }; 
            SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
            , clientManager.Object);
            Response response = service.GetUserById(request);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void TestGetUserById3()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var reviewManager = new Mock<IReviewManager>();
            var tenderManager = new Mock<ITenderManager>();
            var queryManager = new Mock<IQueryManager>();
            var salerMan = new Mock<ISalerManager>();
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            SalerRequest request = new SalerRequest() { UserId = "111", CurrentToken ="12", AuthenticationType = "syf" };
            SalerService service = new SalerService(salerManager.Object, reviewManager.Object, tenderManager.Object, salerMan.Object
            , clientManager.Object);
            Response response = service.GetUserById(request);
            Assert.IsTrue(response.AuthenticationError);
        }
    }
}
