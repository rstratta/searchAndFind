using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchAndFind.DTO;
using SearchAndFind.Core;
using Moq;

namespace SearchAndFind.Services.Test
{
   
    [TestClass]
    public class CategoryServiceTest
    {
        public CategoryServiceTest()
        {
           
        }

        

        [TestMethod]
        public void TestGetAll()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            ICollection<CategoryDTO> categoriesResult = new List<CategoryDTO>();
            categoriesResult.Add(new CategoryDTO());
            CategoryResponse categoryResponse = new CategoryResponse();
            categoryResponse.Categories = categoriesResult;
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var categoryManager = new Mock<ICategoryManager>();
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            categoryManager.Setup(manager => manager.GetAll()).Returns(categoryResponse);
            CategoryService service = new CategoryService(categoryManager.Object, salerManager.Object, clientManager.Object);
            UserRequest request = new UserRequest() { UserId = userId.ToString(), CurrentToken = currentToken.ToString(), AuthenticationType = "syf"};
            Response response = service.GetCategories(request);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestGetAll2()
        {
            Guid userId = Guid.NewGuid();
            Guid currentToken = Guid.NewGuid();
            UserDTO userResult = new UserDTO() { Id = userId, CurrentToken = currentToken.ToString() };
            ICollection<CategoryDTO> categoriesResult = new List<CategoryDTO>();
            categoriesResult.Add(new CategoryDTO());
            var clientManager = new Mock<AbstractUserManager<ClientRequest>>();
            var salerManager = new Mock<AbstractUserManager<SalerRequest>>();
            var categoryManager = new Mock<ICategoryManager>();
            clientManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            salerManager.Setup(manager => manager.GetUserDTOById(userId.ToString())).Returns(userResult);
            CategoryService service = new CategoryService(categoryManager.Object, salerManager.Object, clientManager.Object);
            UserRequest request = new UserRequest() { UserId = userId.ToString(), CurrentToken = "123", AuthenticationType = "syf" };
            Response response = service.GetCategories(request);
            Assert.IsTrue(response.AuthenticationError);
        }
    }
}
