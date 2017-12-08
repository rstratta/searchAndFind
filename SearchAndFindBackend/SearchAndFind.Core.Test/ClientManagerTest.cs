using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchAndFind.Core;
using SearchAndFind.DTO;
using Moq;
using SearchAndFind.Entities;

namespace SearchAndFind.Core.Test
{
    [TestClass]
    public class ClientManagerTest
    {
        [TestMethod]
        public void TestSignUpClient()
        {
            Saler saler = null;
            var clientRepository = new Mock<IUserRepository<Client>>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            salerRepository.Setup(repository => repository.GetUserByMail("test@mail")).Returns(saler);
            clientRepository.Setup(repository => repository.AddObject(new Client()));
            AbstractUserManager<ClientRequest> clientManager = new ClientManager(clientRepository.Object,salerRepository.Object, new ClientDTOBuilder());
            ClientRequest request = new ClientRequest() { DeviceId = "1232", Mail="test@mail",Password="1234",Name="Search", LastName="Find"};
            Response response=clientManager.SignUp(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestSignUpClient2()
        {
            var Mail = "test@mail";
            Saler saler = null;
            var clientRepository = new Mock<IUserRepository<Client>>();
            clientRepository.Setup(repository => repository.AddObject(new Client()));
            var salerRepository = new Mock<IUserRepository<Saler>>();
            salerRepository.Setup(repository => repository.GetUserByMail(Mail)).Returns(saler);
            AbstractUserManager<ClientRequest> clientManager = new ClientManager(clientRepository.Object,salerRepository.Object, new ClientDTOBuilder());
            ClientRequest request = new ClientRequest() { DeviceId = "1232", Mail = Mail, Password = "1234", Name = "Search", LastName = "Find" };
            ClientResponse response = (ClientResponse)clientManager.SignUp(request);
            Assert.AreEqual(Mail, response.ClientDTO.MailAddress);
        }

        [TestMethod]
        public void TestSignUpClient3()
        {
            Saler saler = null;
            var clientRepository = new Mock<IUserRepository<Client>>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            salerRepository.Setup(repository => repository.GetUserByMail("test@mail")).Returns(saler);
            clientRepository.Setup(repository => repository.AddObject(new Client()));
            AbstractUserManager<ClientRequest> clientManager = new ClientManager(clientRepository.Object,salerRepository.Object, new ClientDTOBuilder());
            ClientRequest request = new ClientRequest() { DeviceId = "1232", Mail = "test@mail", Password = "1234", Name = "Search", LastName = "Find" };
            ClientResponse response = (ClientResponse)clientManager.SignUp(request);
            Assert.IsNotNull(response.ClientDTO.CurrentToken);
        }

        [TestMethod]
        public void TestSignInClient()
        {
            var mail = "test@mail";
            Saler saler = null;
            var salerRepository = new Mock<IUserRepository<Saler>>();
            salerRepository.Setup(repository => repository.GetUserByMail(mail)).Returns(saler);
            var clientRepository = new Mock<IUserRepository<Client>>();
            clientRepository.Setup(repository => repository.GetUserByMail(mail)).Returns(new Client() { DeviceId = "1232", MailAddress = mail, Password = EncryptPassword("1234"), Name = "Search", LastName = "Find" });
            clientRepository.Setup(repository => repository.UpdateObject(new Client()));
            AbstractUserManager<ClientRequest> clientManager = new ClientManager(clientRepository.Object,salerRepository.Object, new ClientDTOBuilder());
            ClientRequest request = new ClientRequest() { DeviceId = "1232", Mail = mail, Password = "1234", Name = "Search", LastName = "Find" };
            ClientResponse response = (ClientResponse)clientManager.SignIn(request);
            Assert.IsNotNull(response.ClientDTO.CurrentToken);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestSignInClient2()
        {
            Saler saler = null;
            var mail = "test@mail";
            var clientRepository = new Mock<IUserRepository<Client>>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            salerRepository.Setup(repository => repository.GetUserByMail(mail)).Returns(saler);
            clientRepository.Setup(repository => repository.GetUserByMail(mail)).Returns(new Client() { DeviceId = "1232", MailAddress = mail, Password = "1234", Name = "Search", LastName = "Find" });
            clientRepository.Setup(repository => repository.UpdateObject(new Client()));
            AbstractUserManager<ClientRequest> clientManager = new ClientManager(clientRepository.Object, salerRepository.Object, new ClientDTOBuilder());
            ClientRequest request = new ClientRequest() { DeviceId = "1232", Mail = mail, Password = "1234", Name = "Search", LastName = "Find" };
            clientManager.SignIn(request);
        }



        [TestMethod]
        public void TestSignInClient3()
        {
            var mail = "test@mail";
            var clientRepository = new Mock<IUserRepository<Client>>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            clientRepository.Setup(repository => repository.GetUserByMail(mail)).Returns(new Client() { DeviceId = "1232", MailAddress = mail, Password = EncryptPassword("1234"), Name = "Search", LastName = "Find" });
            clientRepository.Setup(repository => repository.UpdateObject(new Client()));
            AbstractUserManager<ClientRequest> clientManager = new ClientManager(clientRepository.Object,salerRepository.Object, new ClientDTOBuilder());
            ClientRequest request = new ClientRequest() { DeviceId = "1232", Mail = mail, Password = "1234", Name = "Search", LastName = "Find" };
            ClientResponse response = (ClientResponse)clientManager.SignIn(request);
            Assert.IsTrue(response.Success);
        }


        [TestMethod]
        public void TestRemoveAccountClient()
        {
            Guid id = Guid.NewGuid();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            clientRepository.Setup(repository => repository.GetById(id)).Returns(new Client() { Id=id,DeviceId = "1232", MailAddress = "test@test", Password = EncryptPassword("1234"), Name = "Search", LastName = "Find" });
            clientRepository.Setup(repository => repository.UpdateObject(new Client()));
            AbstractUserManager<ClientRequest> clientManager = new ClientManager(clientRepository.Object,salerRepository.Object, new ClientDTOBuilder());
            ClientRequest request = new ClientRequest() { UserId=id.ToString(),DeviceId = "1232", Mail = "test@test", Password = "1234", Name = "Search", LastName = "Find" };
            ClientResponse response = (ClientResponse)clientManager.RemoveAccount(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        [ExpectedException(typeof(ManagerException))]
        public void TestRemoveAccountClient2()
        {
            Guid id = Guid.NewGuid();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            clientRepository.Setup(repository => repository.GetById(id)).Returns(new Client() { Id = id, DeviceId = "1232", MailAddress = "test@test", Password = EncryptPassword("1234"), Name = "Search", LastName = "Find" , Eliminated=true});
            clientRepository.Setup(repository => repository.UpdateObject(new Client()));
            AbstractUserManager<ClientRequest> clientManager = new ClientManager(clientRepository.Object, salerRepository.Object, new ClientDTOBuilder());
            ClientRequest request = new ClientRequest() { UserId = id.ToString(), DeviceId = "1232", Mail = "test@test", Password = "1234", Name = "Search", LastName = "Find" };
            clientManager.RemoveAccount(request);            
        }

        [TestMethod]
        public void TestUpdateAccountClient()
        {
            Guid id = Guid.NewGuid();
            var clientRepository = new Mock<IUserRepository<Client>>();
            var salerRepository = new Mock<IUserRepository<Saler>>();
            clientRepository.Setup(repository => repository.GetById(id)).Returns(new Client() { Id = id, DeviceId = "1232", MailAddress = "test@test", Password = EncryptPassword("1234"), Name = "Search", LastName = "Find" });
            clientRepository.Setup(repository => repository.UpdateObject(new Client()));
            AbstractUserManager<ClientRequest> clientManager = new ClientManager(clientRepository.Object, salerRepository.Object,new ClientDTOBuilder());
            ClientRequest request = new ClientRequest() { UserId = id.ToString(), DeviceId = "1232", Mail = "test@test", Password = "1234", Name = "SearchUpdate", LastName = "Find" };
            ClientResponse response = (ClientResponse)clientManager.UpdateAccount(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestUpdateAccountClient2()
        {
            Guid id = Guid.NewGuid();
            var clientRepository = new Mock<IUserRepository<Client>>();
            clientRepository.Setup(repository => repository.GetById(id)).Returns(new Client() { Id = id, DeviceId = "1232", MailAddress = "test@test", Password = EncryptPassword("1234"), Name = "Search", LastName = "Find", Eliminated = true });
            clientRepository.Setup(repository => repository.UpdateObject(new Client()));
            var salerRepository = new Mock<IUserRepository<Saler>>();
            AbstractUserManager<ClientRequest> clientManager = new ClientManager(clientRepository.Object, salerRepository.Object,new ClientDTOBuilder());
            ClientRequest request = new ClientRequest() { UserId = id.ToString(), DeviceId = "1232", Mail = "test@test", Password = "1234", Name = "SearchTest", LastName = "Find" };
            ClientResponse response=(ClientResponse)clientManager.UpdateAccount(request);
            Assert.AreEqual("SearchTest", response.ClientDTO.Name);
        }


        [TestMethod]
        public void TestGetUserById()
        {
            Guid id = Guid.NewGuid();
            var clientRepository = new Mock<IUserRepository<Client>>();
            clientRepository.Setup(repository => repository.GetById(id)).Returns(new Client() { Id = id, DeviceId = "1232", MailAddress = "test@test", Password = EncryptPassword("1234"), Name = "Search", LastName = "Find", Eliminated = true });
            var salerRepository = new Mock<IUserRepository<Saler>>();
            AbstractUserManager<ClientRequest> clientManager = new ClientManager(clientRepository.Object, salerRepository.Object, new ClientDTOBuilder());
            ClientRequest request = new ClientRequest() { UserId = id.ToString()};
            ClientResponse response = (ClientResponse)clientManager.GetUserById(request);
            Assert.IsTrue(response.Success);
        }

        

        private string EncryptPassword(string password)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(password);
            return System.Convert.ToBase64String(data);
        }
    }
}
