using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public interface ISalerManager
    {
        ICollection<SalerAvailableForTenderDTO> GetSalersNearQueryLocalization(double latitude, double length, DateTime queryDate, Guid categoryId);
        ICollection<SalerCategoryDTO>  GetCategoriesSaler(SalerRequest salerRequest);
        void UpdateCategoriesFromSaler(Guid id, SalerRequest salerRequest);
    }
}
