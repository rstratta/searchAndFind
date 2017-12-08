using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchAndFind.Entities;

namespace SearchAndFind.Repository.Test
{
   
    [TestClass]
    public class CategoryRepositoryTest
    {
        public CategoryRepositoryTest()
        {
            TestUtils.CleanDatabase();
        }

        
        [TestMethod]
        public void TestGetAll()
        {
            CategoryRepository repository = new CategoryRepository();
            Category temporalCategory = new Category() { Name = "categoryName", Description = "categoryDesc" };
            repository.AddObject(temporalCategory);
            ICollection<Category> categories = repository.GetAll();
            Assert.IsTrue(categories.Count > 0);
        }
    }
}
