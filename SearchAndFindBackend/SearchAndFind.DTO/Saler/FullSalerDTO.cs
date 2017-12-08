using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class FullSalerDTO : SalerDTO
    {
        public int ShopHourOpen { get; set; }
        public int ShopHourClose { get; set; }
        public string ShopDaysOpen { get; set; }
        public ICollection<SalerCategoryDTO> SalerCategoryDTO { get; set; }


        public FullSalerDTO(Guid id, string name, string lastName, string mailAddress, string deviceId) : base(id, name, lastName, mailAddress, deviceId){
            SalerCategoryDTO = new List<SalerCategoryDTO>();
        }

        public FullSalerDTO()
        {
        }
    }
}
