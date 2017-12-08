using SearchAndFind.DTO;
using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public interface ISalerRepository :  IUserRepository<Saler>
    {
        ICollection<SalerAvailableForTender> GetSalerNearCardinalCoord(SalerAvailablesToTenderQueryFilter filter);
        Saler GetSalerWithCategories(Guid salerId);
      
    }
}
