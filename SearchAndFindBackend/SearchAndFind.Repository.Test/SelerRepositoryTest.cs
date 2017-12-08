using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchAndFind.Repository;
using SearchAndFind.Entities;
using SearchAndFind.DataAccess;
using SearchAndFind.Core;
using System.Collections.Generic;

namespace SearchAndFind.Repository.Test
{
    [TestClass]
    public class SelerRepositoryTest
    {
        ISalerRepository salerRepository;
        
        public SelerRepositoryTest()
        {
            salerRepository = new SalerRepository();
            TestUtils.CleanDatabase();    
        }

        public Saler CreateSalerTest()
        {
            Saler salerTest = new Saler();
            salerTest.LastName = "De los Santos";
            salerTest.Name = "Juan Manuel";
            salerTest.ShopName = "SI - SI";
            salerTest.MailAddress = "juana@gmail.com";
            salerTest.Password = "Soy un Password";
            salerTest.ShopAddress = "Soy una Direccion";
            salerTest.ShopDaysOpen = "Lunes,Martes,Miercoles,Jueves";
            salerTest.ShopHourClose = 16;
            salerTest.ShopHourOpen = 08;
            salerTest.ShopPhone = "094777888";

            return salerTest;
        }

        
        [TestMethod]
        public void TestAddSalerRepository()
        {

            Saler salerTest = CreateSalerTest();
            salerRepository.AddObject(salerTest);
            Assert.IsTrue(salerTest.Equals(salerRepository.GetById(salerTest.Id)));
         }

        [TestMethod]
        [ExpectedException(typeof(RepositoryException))]
        public void TestAddLocalRepeatSalerRepository()
        {
            Saler salerTest = CreateSalerTest();
            salerRepository.AddObject(salerTest);
            salerRepository.AddObject(salerTest);
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryException))]
        public void TestAddLocalRepeatNameSalerRepository()
        {
            Saler salerTest = CreateSalerTest();
            salerRepository.AddObject(salerTest);

            Saler salerTest2 = CreateSalerTest();
            salerRepository.AddObject(salerTest2);
        }

        [TestMethod]
        public void TestUpdateSalerRepository()
        {
            Saler salerTest = CreateSalerTest();
            salerRepository.AddObject(salerTest);
            salerTest.LastName = "Apellido Nuevo";
            salerTest.MailAddress = "nuevoEmail@gmail.com";
            salerTest.Name = "Nuevo Nombre";
            salerTest.Password = "Nuevo Password";
            salerTest.ShopAddress = "Nueva Direccion";
            salerTest.ShopDaysOpen = "Lunes,Martes,Miercoles,Jueves,Viernes, Sabado";
            salerTest.ShopHourClose = 16;
            salerTest.ShopHourOpen = 08;
            salerTest.ShopPhone = "094852055";

            salerRepository.UpdateObject(salerTest);

            Saler salerObrained = salerRepository.GetById(salerTest.Id);

            Assert.AreEqual(salerTest.LastName,salerObrained.LastName);
            Assert.AreEqual(salerTest.MailAddress,salerObrained.MailAddress);
            Assert.AreEqual(salerTest.Name,salerObrained.Name);
            Assert.AreEqual(salerTest.ShopAddress,salerObrained.ShopAddress);
            Assert.AreEqual(salerTest.ShopDaysOpen,salerObrained.ShopDaysOpen);
            Assert.AreEqual(salerTest.ShopHourClose,salerObrained.ShopHourClose);
            Assert.AreEqual(salerTest.ShopHourOpen,salerObrained.ShopHourOpen);
            Assert.AreEqual(salerTest.ShopPhone,salerObrained.ShopPhone);
        }

        //TODO
        //agregar Categoria

        [TestMethod]
        public void TestRemoveSalerRepository()
        {

            Saler salerTest = CreateSalerTest();
            salerRepository.AddObject(salerTest);
           
            salerRepository.RemoveObject(salerTest);
            Assert.AreEqual(null, salerRepository.GetById(salerTest.Id));
        }
        [TestMethod]
        public void TestGetMailByIDClientRepository()
        {
            Saler salerTest = CreateSalerTest();
            salerRepository.AddObject(salerTest);
            string mailSaler = salerTest.MailAddress;
            Assert.IsTrue(salerTest.Equals(salerRepository.GetUserByMail(mailSaler)));
        }


        [TestMethod]
        public void TestGetSalerByCurrentToken()
        {
            Guid token= Guid.NewGuid();
            Saler salerTest = CreateSalerTest();
            salerTest.CurrentToken = token.ToString();
            salerRepository.AddObject(salerTest);
            Assert.IsNotNull(salerRepository.GetUserByCurrentToken(token.ToString()));
        }

        [TestMethod]
        public void TestGetSalerByCardinalCoords()
        {
            var expectedResult = 1;
            CategoryRepository categoryRepo = new CategoryRepository();
            Category category = new Category("nameTest", "descriptionTest");
            categoryRepo.AddObject(category);
            SalerRepository salerRepository = new SalerRepository();
            Saler salerA = new Saler();
            salerA.Latitude = -34.886705;
            salerA.Length = -56.147660;
            salerA.ShopDaysOpen = "LMXJVSD";
            salerA.ShopHourOpen = 9;
            salerA.ShopHourClose = 18;
            salerRepository.AddObject(salerA);
            salerRepository.AddCategoryOnUser(salerA.Id, category);
            DateTime myDate = DateTime.ParseExact("2017-10-03 14:00:00,000", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
            SalerAvailablesToTenderQueryFilter filter = SalerAvailableQueryFilterBuilder.BuildFilter(-34.882724, -56.151798, myDate, category.Id);
            ICollection<SalerAvailableForTender> result= salerRepository.GetSalerNearCardinalCoord(filter);
            Assert.AreEqual(result.Count, expectedResult);
        }

        [TestMethod]
        public void TestGetSalerByCardinalCoordsOutOfRadius()
        {
            var expectedResult = 0;
            CategoryRepository categoryRepo = new CategoryRepository();
            Category category = new Category("nameTest", "nameDescription");
            categoryRepo.AddObject(category);
            SalerRepository salerRepository = new SalerRepository();
            Saler salerA = new Saler();
            salerA.Latitude = -34.810112;
            salerA.Length = -56.108818;
            salerA.ShopDaysOpen = "LMXJVSD";
            salerA.ShopHourOpen = 9;
            salerA.ShopHourClose = 18;
            salerRepository.AddObject(salerA);
            salerRepository.AddCategoryOnUser(salerA.Id, category);
            SalerAvailablesToTenderQueryFilter filter = SalerAvailableQueryFilterBuilder.BuildFilter(-34.882724, -56.151798, DateTime.Now, category.Id);
            ICollection<SalerAvailableForTender> result = salerRepository.GetSalerNearCardinalCoord(filter);
            Assert.AreEqual(result.Count, expectedResult);
        }
        [TestMethod]
        public void TestGetSalerByCardinalCoordsOutOfHour()
        {
            var expectedResult = 0;
            CategoryRepository categoryRepo = new CategoryRepository();
            Category category = new Category("nameTest", "nameDescription");
            categoryRepo.AddObject(category);
            SalerRepository salerRepository = new SalerRepository();
            Saler salerA = new Saler();
            salerA.Latitude = -34.886705;
            salerA.Length = -56.147660;
            salerA.ShopDaysOpen = "LMXJVSD";
            salerA.ShopHourOpen = 9;
            salerA.ShopHourClose = 12;
            salerRepository.AddObject(salerA);
            salerRepository.AddCategoryOnUser(salerA.Id, category);
            DateTime myDate = DateTime.ParseExact("2017-10-03 21:00:00,000", "yyyy-MM-dd HH:mm:ss,fff",
                                      System.Globalization.CultureInfo.InvariantCulture);
            SalerAvailablesToTenderQueryFilter filter = SalerAvailableQueryFilterBuilder.BuildFilter(-34.882724, -56.151798, myDate, category.Id);
            ICollection<SalerAvailableForTender> result = salerRepository.GetSalerNearCardinalCoord(filter);
            Assert.AreEqual(result.Count, expectedResult);
        }

        [TestMethod]
        public void TestGetSalerByCardinalCoordsOutOfDay()
        {
            var expectedResult = 0;
            CategoryRepository categoryRepo = new CategoryRepository();
            Category category = new Category("nameTest", "nameDescription");
            categoryRepo.AddObject(category);
            SalerRepository salerRepository = new SalerRepository();
            Saler salerA = new Saler();
            salerA.Latitude = -34.886705;
            salerA.Length = -56.147660;
            salerA.ShopDaysOpen = "LVSD";
            salerA.ShopHourOpen = 9;
            salerA.ShopHourClose = 19;
            DateTime myDate = DateTime.ParseExact("2017-10-03 14:00:00,000", "yyyy-MM-dd HH:mm:ss,fff",
                                      System.Globalization.CultureInfo.InvariantCulture);
            salerRepository.AddObject(salerA);
            salerRepository.AddCategoryOnUser(salerA.Id, category);
            SalerAvailablesToTenderQueryFilter filter = SalerAvailableQueryFilterBuilder.BuildFilter(-34.882724, -56.151798, myDate, category.Id);
            ICollection<SalerAvailableForTender> result = salerRepository.GetSalerNearCardinalCoord(filter);
            Assert.AreEqual(result.Count, expectedResult);
        }

    }
}
