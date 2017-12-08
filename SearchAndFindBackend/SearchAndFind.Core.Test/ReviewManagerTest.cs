using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SearchAndFind.Entities;
using SearchAndFind.DTO;

namespace SearchAndFind.Core.Test
{
   
    [TestClass]
    public class ReviewManagerTest
    {
        public ReviewManagerTest()
        {
           
        }

       
        [TestMethod]
        public void TestAddReviewFromClient()
        {
            Guid clientGuid = Guid.NewGuid();
            Client client = new Client();
            client.Id = clientGuid;
            var reviewRepository = new Mock<IReviewRepository>();
            var tenderRepository = new Mock<ITenderRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            Tender tender = new Tender();
            tender.State = Tender.ACEPT_TENDER;
            tender.ClientId = clientGuid;
            tenderRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(tender);
            tenderRepository.Setup(repository => repository.UpdateObject(It.IsAny<Tender>()));
            clientRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(client);
            clientRepository.Setup(repository => repository.UpdateObject(It.IsAny<Client>()));
            reviewRepository.Setup(repository => repository.AddObject(It.IsAny<Review>()));
            IReviewManager reviewManager = new ReviewManager(reviewRepository.Object, clientRepository.Object, salerRepository.Object, tenderRepository.Object, new ReviewDTOBuilder());
            ReviewRequest request = new ReviewRequest() { CurrentToken = Guid.NewGuid().ToString(), UserId = client.Id.ToString(), Points =3, TenderId = clientGuid.ToString() };
            ReviewDTO dto=reviewManager.AddReviewFromClient(request);
            Assert.IsNotNull(dto);
        }


        [TestMethod]
        public void TestAddReviewFromSaler()
        {
            Guid clientGuid = Guid.NewGuid();
            Saler saler = new Saler();
            saler.Id = clientGuid;
            var reviewRepository = new Mock<IReviewRepository>();
            var tenderRepository = new Mock<ITenderRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            Tender tender = new Tender();
            tender.State = Tender.ACEPT_TENDER;
            tender.SalerId = clientGuid;
            tenderRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(tender);
            tenderRepository.Setup(repository => repository.UpdateObject(It.IsAny<Tender>()));
            salerRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(saler);
            salerRepository.Setup(repository => repository.UpdateObject(It.IsAny<Saler>()));
            reviewRepository.Setup(repository => repository.AddObject(It.IsAny<Review>()));
            IReviewManager reviewManager = new ReviewManager(reviewRepository.Object, clientRepository.Object, salerRepository.Object, tenderRepository.Object, new ReviewDTOBuilder());
            ReviewRequest request = new ReviewRequest() { CurrentToken = Guid.NewGuid().ToString(), UserId = saler.Id.ToString(), Points = 3, TenderId = clientGuid.ToString() };
            ReviewDTO dto = reviewManager.AddReviewFromSaler(request);
            Assert.IsNotNull(dto);
        }
    }
}
