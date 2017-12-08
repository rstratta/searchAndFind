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
    public class SalerManagerTest
    {
        public SalerManagerTest()
        { }

        [TestMethod]
        public void TestSignUpSaler()
        {
            
            var salerRepository = new Mock<ISalerRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            Client client = null;
            clientRepository.Setup(repository => repository.GetUserByMail("test@mail")).Returns(client);
            salerRepository.Setup(repository => repository.AddObject(new Saler()));
            AbstractUserManager<SalerRequest> salerManager = new SalerManager(salerRepository.Object, clientRepository.Object, categoryRepository.Object,new FullSalerDTOBuilder(), new CategoryDTOBuilder());
            SalerRequest request = new SalerRequest() { DeviceId = "1232", Mail = "test@mail", Password = "1234", Name = "Search", LastName = "Find" };
            Response response = salerManager.SignUp(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestSignUpSaler2()
        {
            var Mail = "test@mail";
            var salerRepository = new Mock<ISalerRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            Client client = null;
            clientRepository.Setup(repository => repository.GetUserByMail("test@mail")).Returns(client);
            salerRepository.Setup(repository => repository.AddObject(new Saler()));
            AbstractUserManager<SalerRequest> salerManager = new SalerManager(salerRepository.Object, clientRepository.Object, categoryRepository.Object, new FullSalerDTOBuilder(), new CategoryDTOBuilder());
            SalerRequest request = new SalerRequest() { DeviceId = "1232", Mail = Mail, Password = "1234", Name = "Search", LastName = "Find" };
            SalerResponse response = (SalerResponse)salerManager.SignUp(request);
            Assert.AreEqual(Mail, response.SalerDTO.MailAddress);
        }

        [TestMethod]
        public void TestSignUpSaler3()
        {
            var Mail = "test@mail";
            var salerRepository = new Mock<ISalerRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            Client client = null;
            clientRepository.Setup(repository => repository.GetUserByMail("test@mail")).Returns(client);
            salerRepository.Setup(repository => repository.AddObject(new Saler()));
            AbstractUserManager<SalerRequest> salerManager = new SalerManager(salerRepository.Object, clientRepository.Object, categoryRepository.Object, new FullSalerDTOBuilder(), new CategoryDTOBuilder());
            SalerRequest request = new SalerRequest() { DeviceId = "1232", Mail = Mail, Password = "1234", Name = "Search", LastName = "Find" };
            SalerResponse response = (SalerResponse)salerManager.SignUp(request);
            Assert.IsNotNull(response.SalerDTO.CurrentToken);
        }

        [TestMethod]
        public void TestSignInSaler()
        {
            var mail = "test@mail";
            var salerRepository = new Mock<ISalerRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            salerRepository.Setup(repository => repository.GetUserByMail(mail)).Returns(new Saler() { DeviceId = "1232", MailAddress = mail, Password = EncryptPassword("1234"), Name = "Search", LastName = "Find" });
            salerRepository.Setup(repository => repository.UpdateObject(new Saler()));
            AbstractUserManager<SalerRequest> salerManager = new SalerManager(salerRepository.Object, clientRepository.Object, categoryRepository.Object, new FullSalerDTOBuilder(), new CategoryDTOBuilder());
            SalerRequest request = new SalerRequest() { DeviceId = "1232", Mail = mail, Password = "1234", Name = "Search", LastName = "Find" };
            SalerResponse response = (SalerResponse)salerManager.SignIn(request);
            Assert.IsNotNull(response.SalerDTO.CurrentToken);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestSignInSaler2()
        {
            var mail = "test@mail";
            var salerRepository = new Mock<ISalerRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            salerRepository.Setup(repository => repository.GetUserByMail(mail)).Returns(new Saler() { DeviceId = "1232", MailAddress = mail, Password = EncryptPassword("1234"), Name = "Search", LastName = "Find", Eliminated=true });
            salerRepository.Setup(repository => repository.UpdateObject(new Saler()));
            AbstractUserManager<SalerRequest> salerManager = new SalerManager(salerRepository.Object, clientRepository.Object, categoryRepository.Object, new FullSalerDTOBuilder(), new CategoryDTOBuilder());
            SalerRequest request = new SalerRequest() { DeviceId = "1232", Mail = mail, Password = "wrong", Name = "Search", LastName = "Find" };
            salerManager.SignIn(request);
        }



        [TestMethod]
        public void TestSignInSaler3()
        {
            var mail = "test@mail";
            var salerRepository = new Mock<ISalerRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            salerRepository.Setup(repository => repository.GetUserByMail(mail)).Returns(new Saler() { DeviceId = "1232", MailAddress = mail, Password = EncryptPassword("1234"), Name = "Search", LastName = "Find" });
            salerRepository.Setup(repository => repository.UpdateObject(new Saler()));
            AbstractUserManager<SalerRequest> salerManager = new SalerManager(salerRepository.Object, clientRepository.Object, categoryRepository.Object, new FullSalerDTOBuilder(), new CategoryDTOBuilder());
            SalerRequest request = new SalerRequest() { DeviceId = "1232", Mail = mail, Password = "1234", Name = "Search", LastName = "Find" };
            SalerResponse response = (SalerResponse)salerManager.SignIn(request);
            Assert.IsTrue(response.Success);
        }


        [TestMethod]
        public void TestRemoveAccountSaler()
        {
            Guid id = Guid.NewGuid();
            var salerRepository = new Mock<ISalerRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            salerRepository.Setup(repository => repository.GetById(id)).Returns(new Saler() { Id = id, DeviceId = "1232", MailAddress = "test@test", Password = EncryptPassword("1234"), Name = "Search", LastName = "Find" });
            salerRepository.Setup(repository => repository.UpdateObject(new Saler()));
            AbstractUserManager<SalerRequest> salerManager = new SalerManager(salerRepository.Object,clientRepository.Object, categoryRepository.Object, new FullSalerDTOBuilder(), new CategoryDTOBuilder());
            SalerRequest request = new SalerRequest() { UserId = id.ToString(), DeviceId = "1232", Mail = "test@test", Password = "1234", Name = "Search", LastName = "Find" };
            SalerResponse response = (SalerResponse)salerManager.RemoveAccount(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestRemoveAccountSaler2()
        {
            Guid id = Guid.NewGuid();
            var salerRepository = new Mock<ISalerRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            salerRepository.Setup(repository => repository.GetById(id)).Returns(new Saler() { Id = id, DeviceId = "1232", MailAddress = "test@test", Password = EncryptPassword("1234"), Name = "Search", LastName = "Find", Eliminated = true });
            salerRepository.Setup(repository => repository.UpdateObject(new Saler()));
            AbstractUserManager<SalerRequest> salerManager = new SalerManager(salerRepository.Object, clientRepository.Object, categoryRepository.Object, new FullSalerDTOBuilder(), new CategoryDTOBuilder());
            SalerRequest request = new SalerRequest() { UserId = id.ToString(), DeviceId = "1232", Mail = "test@test", Password = "1234", Name = "Search", LastName = "Find" };
            salerManager.RemoveAccount(request);
        }

        [TestMethod]
        public void TestUpdateAccountSaler()
        {
            Guid id = Guid.NewGuid();
            var salerRepository = new Mock<ISalerRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            salerRepository.Setup(repository => repository.GetById(id)).Returns(new Saler() { Id = id, DeviceId = "1232", MailAddress = "test@test", Password = EncryptPassword("1234"), Name = "Search", LastName = "Find" });
            salerRepository.Setup(repository => repository.UpdateObject(new Saler()));
            AbstractUserManager<SalerRequest> salerManager = new SalerManager(salerRepository.Object, clientRepository.Object, categoryRepository.Object, new FullSalerDTOBuilder(), new CategoryDTOBuilder());
            SalerRequest request = new SalerRequest() { UserId = id.ToString(), DeviceId = "1232", Mail = "test@test", Password = "1234", Name = "SearchUpdate", LastName = "Find" };
            SalerResponse response = (SalerResponse)salerManager.UpdateAccount(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestUpdateAccountSaler2()
        {
            Guid id = Guid.NewGuid();
            var salerRepository = new Mock<ISalerRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            salerRepository.Setup(repository => repository.GetById(id)).Returns(new Saler() { Id = id, DeviceId = "1232", MailAddress = "test@test", Password = EncryptPassword("1234"), Name = "Search", LastName = "Find", Eliminated = true });
            salerRepository.Setup(repository => repository.UpdateObject(new Saler()));
            AbstractUserManager<SalerRequest> salerManager = new SalerManager(salerRepository.Object,clientRepository.Object, categoryRepository.Object, new FullSalerDTOBuilder(), new CategoryDTOBuilder());
            SalerRequest request = new SalerRequest() { UserId = id.ToString(), DeviceId = "1232", Mail = "test@test", Password = "1234", Name = "SearchTest", LastName = "Find" };
            SalerResponse response = (SalerResponse)salerManager.UpdateAccount(request);
            Assert.AreEqual("SearchTest", response.SalerDTO.Name);
        }

        [TestMethod]
        public void UpdateAccountSuccess()
        {
            Guid id = Guid.NewGuid();
            var salerRepository = new Mock<ISalerRepository>();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var categoryRepository = new Mock<ICategoryRepository>();
            salerRepository.Setup(repository => repository.GetById(id)).Returns(new Saler() { Id = id, DeviceId = "1232", MailAddress = "test@test", Password = EncryptPassword("1234"), Name = "Search", LastName = "Find", Eliminated = false, ShopDaysOpen= "LMXJV",
            ShopName = "TATA", ShopHourOpen = 9, ShopHourClose = 18,ShopPhone = "23648754"});
            salerRepository.Setup(repository => repository.UpdateObject(new Saler()));
            AbstractUserManager<SalerRequest> salerManager = new SalerManager(salerRepository.Object, clientRepository.Object, categoryRepository.Object, new FullSalerDTOBuilder(), new CategoryDTOBuilder());
            SalerRequest request = new SalerRequest() { UserId = id.ToString(), DeviceId = "1232", Mail = "test@test", Password = "1234", Name = "SearchTest", LastName = "Find" };
            SalerResponse response = (SalerResponse)salerManager.UpdateAccount(request);
            Assert.IsTrue(response.Success);
        }

        private string EncryptPassword(string password)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(password);
            return System.Convert.ToBase64String(data);
        }

        [TestMethod]
        public void TestGetUserById()
        {
            Guid id = Guid.NewGuid();
            var salerRepository = new Mock<ISalerRepository>();
            var categoryRepository = new Mock<ICategoryRepository>();
            salerRepository.Setup(repository => repository.GetById(id)).Returns(new Saler() { Id = id, DeviceId = "1232", MailAddress = "test@test", Password = EncryptPassword("1234"), Name = "Search", LastName = "Find", Eliminated = true });
            var clientRepository = new Mock<IUserRepository<Client>>();
            AbstractUserManager<SalerRequest> salerManager = new SalerManager(salerRepository.Object, clientRepository.Object, categoryRepository.Object,new FullSalerDTOBuilder(), new CategoryDTOBuilder());
            SalerRequest request = new SalerRequest() { UserId = id.ToString() };
            SalerResponse response = (SalerResponse)salerManager.GetUserById(request);
            Assert.IsTrue(response.Success);
        }
    }
}
