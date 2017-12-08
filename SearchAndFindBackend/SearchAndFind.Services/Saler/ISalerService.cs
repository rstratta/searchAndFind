using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public interface ISalerService:IUserService<SalerRequest>
    {
        Response GetAvailableCategories(SalerRequest request);
        Response UpdateCategoriesFromSeler(SalerRequest salerRequest);

        Response UpdateAccount(SalerRequest request);
    }
}
