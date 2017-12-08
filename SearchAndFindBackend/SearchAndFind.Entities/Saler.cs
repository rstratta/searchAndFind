using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Entities
{
    public class Saler : User
    {
        public double Latitude { get; set; }
        public double Length { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string ShopPhone { get; set; }
        public int ShopHourOpen { get; set; }
        public int ShopHourClose { get; set; }
        public string ShopDaysOpen { get; set; }
        


        public Saler()
        {
            Id = Guid.NewGuid();
            Categories = new List<Category>();
        }

        public Saler(string deviceId, string name, string lastName, string mailAddress) : this()
        {
            DeviceId = deviceId;
            Name = name;
            LastName = lastName;
            MailAddress = mailAddress;
            Id = Guid.NewGuid();
            Categories = new List<Category>();
            ShopAddress = "";
            ShopDaysOpen = "";
            ShopName = "";

        }


        public override bool Equals(object obj)
        {
            return Id.Equals(((Saler)obj).Id);
        }
    }
}
