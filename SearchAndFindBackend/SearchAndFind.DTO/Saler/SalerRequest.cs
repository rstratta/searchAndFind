using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class SalerRequest : UserRequest
    {
        public double Latitude { get; set; }
        public double Length { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string ShopPhone { get; set; }
        public int ShopHourOpen { get; set; }
        public int ShopHourClose { get; set; }
        public string ShopDaysOpen { get; set; }
        public virtual ICollection<CategoryDTO> Categories { get; set; }
        public ICollection<SalerCategoryDTO> SalerCategoryDTO { get; set; }
    }
}
