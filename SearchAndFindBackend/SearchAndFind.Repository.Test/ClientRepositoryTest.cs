using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchAndFind.Entities;
using SearchAndFind.Core;

namespace SearchAndFind.Repository.Test
{
    [TestClass]
    public class ClientRepositoryTest
    {
        IUserRepository<Client> clientRepository;
        public ClientRepositoryTest()
        {
            clientRepository = new ClientRepository();
            TestUtils.CleanDatabase();
        }
        public Client CreateClientTest()
        {
            Client clientTest = new Client();
            clientTest.LastName = "De los Santos";
            clientTest.Name = "Juan Manuel";
            clientTest.MailAddress = "juana@gmail.com";
            clientTest.Password = "Soy un Password";

            return clientTest;
        }
        [TestMethod]
        public void TestAddClientRepository()
        {
            Client clientTest = CreateClientTest();
            clientRepository.AddObject(clientTest);
            Assert.IsTrue(clientTest.Equals(clientRepository.GetById(clientTest.Id)));
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryException))]
        public void TestAddLocalRepeatClientRepository()
        {
            Client clientTest = CreateClientTest();
            clientRepository.AddObject(clientTest);
            clientRepository.AddObject(clientTest);
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryException))]
        public void TestAddLocalRepeatNameClientRepository()
        {
            Client clientTest = CreateClientTest();
            clientRepository.AddObject(clientTest);

            Client clientTest2 = CreateClientTest();
            clientRepository.AddObject(clientTest2);
        }
        [TestMethod]
        public void TestUpdateClientRepository()
        {
            Client clientTest = CreateClientTest();
            clientRepository.AddObject(clientTest);
            clientTest.LastName = "Apellido Nuevo";
            clientTest.MailAddress = "nuevoEmail@gmail.com";
            clientTest.Name = "Nuevo Nombre";
            clientTest.Password = "Nuevo Password";

            clientRepository.UpdateObject(clientTest);

            Client clientObtained = clientRepository.GetById(clientTest.Id);

            Assert.AreEqual(clientTest.LastName,clientObtained.LastName);
            Assert.AreEqual(clientTest.MailAddress, clientObtained.MailAddress);
            Assert.AreEqual(clientTest.Name, clientObtained.Name);
            Assert.AreEqual(clientTest.Password, clientObtained.Password);
        }
        

        [TestMethod]
        public void TestRemoveClientRepository()
        {
            Client clientTest = CreateClientTest();
            clientRepository.AddObject(clientTest);

            clientRepository.RemoveObject(clientTest);
            Assert.AreEqual(null, clientRepository.GetById(clientTest.Id));
        }

        [TestMethod]
        public void TestGetMailByIDClientRepository()
        {
            Client clientTest = CreateClientTest();
            clientRepository.AddObject(clientTest);
            string mailClient = clientTest.MailAddress;
            Assert.IsTrue(clientTest.Equals(clientRepository.GetUserByMail(mailClient)));
        }

        [TestMethod]
        public void TestGetClientByCurrentToken()
        {
            Guid token = Guid.NewGuid();
            Client client = CreateClientTest();
            client.CurrentToken = token.ToString();
            clientRepository.AddObject(client);
            Assert.IsNotNull(clientRepository.GetUserByCurrentToken(token.ToString()));
        }
    }
}
