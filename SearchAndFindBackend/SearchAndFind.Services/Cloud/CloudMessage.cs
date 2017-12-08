using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class CloudMessage
    {
        public string to { get; set; }
        public Message notification { get; set; }
        public Dictionary<string,string> data { get; set; }

        
    }
}
