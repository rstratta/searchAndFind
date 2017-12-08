using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public class SalerAvailableForTender

    {
        public Guid SalerId { get; set; }
        public double Latitude { get; set; }
        public double Length { get; set; }
        public string SalerName { get; set; }
        public string ShopName { get; set; }
        public string ShopPhone { get; set; }
        public string ShopAddress { get; set; }
        public string DeviceId { get; set; }
        public double Distance { get; set; }
    }
}
