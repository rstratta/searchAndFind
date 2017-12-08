using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Entities
{
    public class Client : User
    {
        public virtual ICollection<Query> Queries { get; set; }
        

        public Client()
        {
            Id = Guid.NewGuid();
        }

        public Client(string deviceId, string name, string lastName, string mailAddress) : this()
        {
            DeviceId = deviceId;
            Name = name;
            LastName = lastName;
            MailAddress = mailAddress;
        }

        
    }
}
