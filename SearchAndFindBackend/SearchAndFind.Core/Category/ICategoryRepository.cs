using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetCategoryByName(string categoryName);

        ICollection<Category> GetAll();
    }
}
