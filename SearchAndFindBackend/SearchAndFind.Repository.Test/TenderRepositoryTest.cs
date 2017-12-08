using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchAndFind.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SearchAndFind.Repository.Test
{
    [TestClass]
    public class TenderRepositoryTest
    {
        public Guid guidTest = Guid.NewGuid();
        public Guid clientId = Guid.NewGuid();
        
        public TenderRepositoryTest()
        {
            TestUtils.CleanDatabase();
            Query queryTest = new Query();
            queryTest.Id = guidTest;
            
            queryTest.CategoryId = guidTest;
            Category categoryTest = new Category();
            categoryTest.Id = guidTest;
            Saler saler = new Saler();
            saler.MailAddress = "salerMail";
            saler.Id = guidTest;
            Client client = new Client();
            client.Id = clientId;
            client.MailAddress = "clientMail";
            queryTest.ClientId = clientId;
            CategoryRepository categoryRepo = new CategoryRepository();
            categoryRepo.AddObject(categoryTest);
            SalerRepository salerRepo = new SalerRepository();
            salerRepo.AddObject(saler);
            ClientRepository clientRepo = new ClientRepository();
            QueryRepository queryRepo = new QueryRepository();
            clientRepo.AddObject(client);
            queryRepo.AddObject(queryTest);
            
            
        }

        [TestMethod]
        public void TestAddTenderAndImages()
        {
            
            Tender tender = new Tender();
            TenderImage image = new TenderImage() { EncodedImage = "test" };
            tender.Images.Add(image);
            tender.SalerId = guidTest;
            tender.QueryId = guidTest;
            tender.ClientId = clientId;
            TenderRepository repository = new TenderRepository();
            repository.AddObject(tender);
            Tender result = repository.GetById(tender.Id);
            Assert.IsTrue(result.Images.Count == 1);
        }

        [TestMethod]
        public void TestGetAceptedTendersByClientId()
        {

            Tender tender = new Tender();
            TenderImage image = new TenderImage() { EncodedImage = "test" };
            tender.Images.Add(image);
            tender.SalerId = guidTest;
            tender.QueryId = guidTest;
            tender.ClientId = clientId;
            TenderRepository repository = new TenderRepository();
            repository.AddObject(tender);
            tender.State = Tender.ACEPT_TENDER;
            repository.UpdateObject(tender);
            ICollection<Tender> result= repository.GetAceptedTendersByClientId(clientId);
            Assert.IsTrue(result.Count == 1);
        }

        [TestMethod]
        public void TestGetAceptedTendersByClientId2()
        {

            Tender tender = new Tender();
            TenderImage image = new TenderImage() { EncodedImage = "test" };
            tender.Images.Add(image);
            tender.SalerId = guidTest;
            tender.QueryId = guidTest;
            tender.ClientId = clientId;
            TenderRepository repository = new TenderRepository();
            repository.AddObject(tender);
            repository.UpdateObject(tender);
            ICollection<Tender> result = repository.GetAceptedTendersByClientId(clientId);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void TestGetAceptedTendersBySalerId()
        {

            Tender tender = new Tender();
            TenderImage image = new TenderImage() { EncodedImage = "test" };
            tender.Images.Add(image);
            tender.SalerId = guidTest;
            tender.QueryId = guidTest;
            tender.ClientId = clientId;
            TenderRepository repository = new TenderRepository();
            repository.AddObject(tender);
            tender.State = Tender.ACEPT_TENDER;
            repository.UpdateObject(tender);
            ICollection<Tender> result = repository.GetAceptedTendersBySalerId(guidTest);
            Assert.IsTrue(result.Count == 1);
        }

        [TestMethod]
        public void TestGetAceptedTendersBySalerId2()
        {

            Tender tender = new Tender();
            TenderImage image = new TenderImage() { EncodedImage = "test" };
            tender.Images.Add(image);
            tender.SalerId = guidTest;
            tender.QueryId = guidTest;
            tender.ClientId = clientId;
            TenderRepository repository = new TenderRepository();
            repository.AddObject(tender);
            repository.UpdateObject(tender);
            ICollection<Tender> result = repository.GetAceptedTendersBySalerId(guidTest);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void TestGetTenderBySalerIdAndQueryId()
        {

            Tender tender = new Tender();
            TenderImage image = new TenderImage() { EncodedImage = "test" };
            tender.Images.Add(image);
            tender.SalerId = guidTest;
            tender.QueryId = guidTest;
            tender.ClientId = clientId;
            TenderRepository repository = new TenderRepository();
            repository.AddObject(tender);
            Tender result = repository.GetTenderBySalerIdAndQueryId(guidTest,guidTest);
            Assert.IsNotNull(result);
        }


    }
}
