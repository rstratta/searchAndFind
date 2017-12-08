using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class ReviewRequest : Request
    {
        public int Points { get; set; }
        public string TenderId { get; set; }
        
    }
}
