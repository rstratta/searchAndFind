using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public interface IUserService<R>
    {
        Response SignIn(R request);
        Response SignUp(R request);
        Response GetUserById(R request);
    }
}
