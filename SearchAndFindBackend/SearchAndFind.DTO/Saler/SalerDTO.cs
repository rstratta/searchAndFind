using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class SalerDTO:UserDTO
    {
       
        public double Latitude { get; set; }
        public double Length { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string ShopPhone { get; set; }
        public SalerDTO(Guid id, string name, string lastName, string mailAddress, string deviceId) : base(id, name, lastName, mailAddress, deviceId)
        {
        }

        public SalerDTO()
        {
        }
    }
}
