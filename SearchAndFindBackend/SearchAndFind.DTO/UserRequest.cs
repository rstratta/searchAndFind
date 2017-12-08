using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class UserRequest : Request
    {
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
        
    }
}
