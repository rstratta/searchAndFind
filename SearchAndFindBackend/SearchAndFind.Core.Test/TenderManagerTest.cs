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
    public class TenderManagerTest
    {
       

        [TestMethod]
        public void TestDoTender()
        {
             Guid guidTest = Guid.NewGuid();
            Query query = new Query();
            query.Id = guidTest;
            var queryRepository = new Mock<IQueryRepository>();
            var tenderRepository = new Mock<ITenderRepository>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            var reviewRepository = new Mock<IReviewRepository>();
            reviewRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Review());
            salerRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Saler());
            queryRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(query);
            tenderRepository.Setup(repository => repository.AddObject(It.IsAny<Tender>()));
            
            tenderRepository.Setup(repository => repository.GetTenderBySalerIdAndQueryId(It.IsAny<Guid>(), It.IsAny<Guid>()));
            tenderRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Tender());
            ITenderManager tenderManager = new TenderManager(tenderRepository.Object,queryRepository.Object, new TenderDTOBuilder(),salerRepository.Object, new FullSalerDTOBuilder(), reviewRepository.Object);
            TenderRequest request = new TenderRequest() { QueryId=guidTest.ToString(),UserId=guidTest.ToString() };
            TenderDTO dto = tenderManager.DoTender(request);
            Assert.IsNotNull(dto);
        }

        [TestMethod]
        [ExpectedException (typeof(ManagerException))]
        public void TestDoTender2()
        {
            Guid guidTest = Guid.NewGuid();
            var queryRepository = new Mock<IQueryRepository>();
            var tenderRepository = new Mock<ITenderRepository>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            var reviewRepository = new Mock<IReviewRepository>();
            reviewRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Review());
            salerRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Saler());
            tenderRepository.Setup(repository => repository.AddObject(It.IsAny<Tender>()));
            
            tenderRepository.Setup(repository => repository.GetTenderBySalerIdAndQueryId(It.IsAny<Guid>(), It.IsAny<Guid>()));
            tenderRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Tender() );
            ITenderManager tenderManager = new TenderManager(tenderRepository.Object,queryRepository.Object, new TenderDTOBuilder(),salerRepository.Object, new FullSalerDTOBuilder(), reviewRepository.Object);
            TenderRequest request = new TenderRequest() { QueryId = guidTest.ToString(), UserId = guidTest.ToString() };
            tenderManager.DoTender(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestDoTender3()
        {
            Guid guidTest = Guid.NewGuid();
            Query query = new Query();
            query.State = Query.CANCELED_STATE;
            query.Id = guidTest;
            var queryRepository = new Mock<IQueryRepository>();
            var tenderRepository = new Mock<ITenderRepository>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            var reviewRepository = new Mock<IReviewRepository>();
            reviewRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Review());
            salerRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Saler());
            queryRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(query);
            tenderRepository.Setup(repository => repository.AddObject(It.IsAny<Tender>()));
            
            tenderRepository.Setup(repository => repository.GetTenderBySalerIdAndQueryId(It.IsAny<Guid>(), It.IsAny<Guid>()));
            tenderRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Tender() );
            ITenderManager tenderManager = new TenderManager(tenderRepository.Object,queryRepository.Object, new TenderDTOBuilder(),salerRepository.Object, new FullSalerDTOBuilder(), reviewRepository.Object);
            TenderRequest request = new TenderRequest() { QueryId = guidTest.ToString(), UserId = guidTest.ToString() };
            tenderManager.DoTender(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestDoTender4()
        {
            Guid guidTest = Guid.NewGuid();
            Query query = new Query();
            query.State = Query.CONFIRMED_STATE;
            query.Id = guidTest;
            var queryRepository = new Mock<IQueryRepository>();
            var tenderRepository = new Mock<ITenderRepository>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            var reviewRepository = new Mock<IReviewRepository>();
            reviewRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Review());
            salerRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Saler());
            queryRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(query);
            tenderRepository.Setup(repository => repository.AddObject(It.IsAny<Tender>()));
            
            tenderRepository.Setup(repository => repository.GetTenderBySalerIdAndQueryId(It.IsAny<Guid>(), It.IsAny<Guid>()));
            tenderRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Tender() );
            ITenderManager tenderManager = new TenderManager(tenderRepository.Object,queryRepository.Object, new TenderDTOBuilder(),salerRepository.Object, new FullSalerDTOBuilder(), reviewRepository.Object);
            TenderRequest request = new TenderRequest() { QueryId = guidTest.ToString(), UserId = guidTest.ToString() };
            tenderManager.DoTender(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestDoTender5()
        {
            Guid guidTest = Guid.NewGuid();
            Query query = new Query();
            query.Id = guidTest;
            var queryRepository = new Mock<IQueryRepository>();
            var tenderRepository = new Mock<ITenderRepository>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            var reviewRepository = new Mock<IReviewRepository>();
            reviewRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Review());
            salerRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Saler());
            queryRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(query);
            tenderRepository.Setup(repository => repository.AddObject(It.IsAny<Tender>()));
            
            tenderRepository.Setup(repository => repository.GetTenderBySalerIdAndQueryId(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(new Tender());
            tenderRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Tender() );
            ITenderManager tenderManager = new TenderManager(tenderRepository.Object,queryRepository.Object, new TenderDTOBuilder(),salerRepository.Object, new FullSalerDTOBuilder(), reviewRepository.Object);
            TenderRequest request = new TenderRequest() { QueryId = guidTest.ToString(), UserId = guidTest.ToString() };
            tenderManager.DoTender(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestUpdateTenderState()
        {
            Guid guidTest = Guid.NewGuid();
            Tender tender = new Tender();
            tender.Id = guidTest;
            tender.State = Tender.ACEPT_TENDER;
            var queryRepository = new Mock<IQueryRepository>();
            var tenderRepository = new Mock<ITenderRepository>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            var reviewRepository = new Mock<IReviewRepository>();
            reviewRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Review());
            salerRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Saler());
            tenderRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(tender);
            ITenderManager tenderManager = new TenderManager(tenderRepository.Object,queryRepository.Object, new TenderDTOBuilder(),salerRepository.Object, new FullSalerDTOBuilder(), reviewRepository.Object);
            TenderRequest request = new TenderRequest() { TenderId = guidTest.ToString(), UserId = guidTest.ToString() , QueryId=guidTest.ToString()};
            tenderManager.ConfirmTender(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestUpdateTenderState2()
        {
            Guid guidTest = Guid.NewGuid();
            Tender tender = new Tender();
            tender.Id = guidTest;
            tender.State = Tender.ACEPT_TENDER;
            var queryRepository = new Mock<IQueryRepository>();
            var tenderRepository = new Mock<ITenderRepository>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            var reviewRepository = new Mock<IReviewRepository>();
            reviewRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Review());
            salerRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Saler());
            tenderRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(tender);
            ITenderManager tenderManager = new TenderManager(tenderRepository.Object,queryRepository.Object, new TenderDTOBuilder(),salerRepository.Object, new FullSalerDTOBuilder(), reviewRepository.Object);
            TenderRequest request = new TenderRequest() { TenderId = guidTest.ToString(), UserId = guidTest.ToString() };
            tenderManager.RevokeTender(request);
        }

        [TestMethod]
        public void TestUpdateTenderState3()
        {
            Guid guidTest = Guid.NewGuid();
            Tender tender = new Tender();
            tender.Id = guidTest;
            tender.QueryId = guidTest;
            var queryRepository = new Mock<IQueryRepository>();
            var tenderRepository = new Mock<ITenderRepository>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            var reviewRepository = new Mock<IReviewRepository>();
            reviewRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Review());
            salerRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Saler());
            tenderRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(tender);
            tenderRepository.Setup(repository => repository.GetTendersByQueryId(It.IsAny<Guid>())).Returns(new List<Tender>());
            queryRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(new Query());
            ITenderManager tenderManager = new TenderManager(tenderRepository.Object,queryRepository.Object, new TenderDTOBuilder(),salerRepository.Object, new FullSalerDTOBuilder(), reviewRepository.Object);
            TenderRequest request = new TenderRequest() { TenderId = guidTest.ToString(),TenderState=Tender.ACEPT_TENDER, UserId = guidTest.ToString() , QueryId = guidTest.ToString() };
            TenderDTO dto=tenderManager.ConfirmTender(request);
            Assert.IsNotNull(dto);
        }
    }
}
