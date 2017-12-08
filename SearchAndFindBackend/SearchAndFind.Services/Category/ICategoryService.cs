using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public interface ICategoryService
    {
        Response GetCategories(UserRequest request);
    }
}
