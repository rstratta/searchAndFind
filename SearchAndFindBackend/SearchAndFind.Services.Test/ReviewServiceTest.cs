using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchAndFind.DTO;
using Moq;
using SearchAndFind.Core;

namespace SearchAndFind.Services.Test
{
   
    [TestClass]
    public class ReviewServiceTest
    {
        public ReviewServiceTest()
        {
        }

       

        [TestMethod]
        public void TestaddReviewFromClient()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            ReviewDTO reviewDTO = new ReviewDTO() { DestinationId = userId };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var reviewManager = new Mock<IReviewManager>();
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            reviewManager.Setup(manager => manager.AddReviewFromClient(It.IsAny<ReviewRequest>())).Returns(reviewDTO);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            ReviewService service = new ReviewService(reviewManager.Object,  cloudMessageSender.Object, clientManager.Object, salerManager.Object);
            ReviewRequest request = new ReviewRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf",  Points = 2 };
            Response response = service.addReviewFromClient(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestaddReviewFromClient2()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            ReviewDTO reviewDTO = new ReviewDTO() { DestinationId = userId };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var reviewManager = new Mock<IReviewManager>();
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            reviewManager.Setup(manager => manager.AddReviewFromClient(It.IsAny<ReviewRequest>())).Returns(reviewDTO);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            ReviewService service = new ReviewService(reviewManager.Object,  cloudMessageSender.Object, clientManager.Object, salerManager.Object);
            ReviewRequest request = new ReviewRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Points = 0 };
            Response response = service.addReviewFromClient(request);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void TestaddReviewFromClient3()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            ReviewDTO reviewDTO = new ReviewDTO() { DestinationId = userId };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var reviewManager = new Mock<IReviewManager>();
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            reviewManager.Setup(manager => manager.AddReviewFromClient(It.IsAny<ReviewRequest>())).Returns(reviewDTO);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            ReviewService service = new ReviewService(reviewManager.Object,  cloudMessageSender.Object, clientManager.Object, salerManager.Object);
            ReviewRequest request = new ReviewRequest() { UserId = userId.ToString(), CurrentToken = "a", AuthenticationType = "syf", Points = 2 };
            Response response = service.addReviewFromClient(request);
            Assert.IsTrue(response.AuthenticationError);
        }


        [TestMethod]
        public void TestaddReviewFromSaler()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            ReviewDTO reviewDTO = new ReviewDTO() { DestinationId = userId };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var reviewManager = new Mock<IReviewManager>();
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            reviewManager.Setup(manager => manager.AddReviewFromSaler(It.IsAny<ReviewRequest>())).Returns(reviewDTO);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            ReviewService service = new ReviewService(reviewManager.Object,  cloudMessageSender.Object, clientManager.Object, salerManager.Object);
            ReviewRequest request = new ReviewRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Points = 2 };
            Response response = service.addReviewFromSaler(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestaddReviewFromSaler2()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            ReviewDTO reviewDTO = new ReviewDTO() { DestinationId = userId };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var reviewManager = new Mock<IReviewManager>();
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            reviewManager.Setup(manager => manager.AddReviewFromSaler(It.IsAny<ReviewRequest>())).Returns(reviewDTO);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            ReviewService service = new ReviewService(reviewManager.Object,  cloudMessageSender.Object, clientManager.Object, salerManager.Object);
            ReviewRequest request = new ReviewRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf", Points = 0 };
            Response response = service.addReviewFromSaler(request);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void TestaddReviewFromSaler3()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            ReviewDTO reviewDTO = new ReviewDTO() { DestinationId = userId };
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var cloudMessageSender = new Mock<ICloudMessageSender>();
            var reviewManager = new Mock<IReviewManager>();
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            reviewManager.Setup(manager => manager.AddReviewFromSaler(It.IsAny<ReviewRequest>())).Returns(reviewDTO);
            cloudMessageSender.Setup(messageSender => messageSender.SendMessage(It.IsAny<CloudMessage>()));
            ReviewService service = new ReviewService(reviewManager.Object,  cloudMessageSender.Object, clientManager.Object, salerManager.Object);
            ReviewRequest request = new ReviewRequest() { UserId = userId.ToString(), CurrentToken = "a", AuthenticationType = "syf", Points = 2 };
            Response response = service.addReviewFromSaler(request);
            Assert.IsTrue(response.AuthenticationError);
        }
    }
}
