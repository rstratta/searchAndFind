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
    public class CategoryManagerTest
    {
        public CategoryManagerTest()
        {
            
        }       

        [TestMethod]
        public void TestGetAll()
        {
            ICollection<Category> categoryList = new List<Category>();
            categoryList.Add(new Category() { Id = Guid.NewGuid(), Description = "categoryDesc", Name = "category" });
            var categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository.Setup(repository => repository.GetAll()).Returns(categoryList);
            CategoryManager categoryManager = new CategoryManager(categoryRepository.Object, new CategoryDTOBuilder());
            Response result = categoryManager.GetAll();
            Assert.IsTrue(result.Success);
        }
    }
}
