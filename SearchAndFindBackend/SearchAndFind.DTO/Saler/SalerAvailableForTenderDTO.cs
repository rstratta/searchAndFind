using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class SalerAvailableForTenderDTO 
    {
        public double Latitude { get; set; }
        public double Length { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string ShopPhone { get; set; }
        public double Distance { get; set; }
        public string DeviceId { get; set; }
       
    }
}
