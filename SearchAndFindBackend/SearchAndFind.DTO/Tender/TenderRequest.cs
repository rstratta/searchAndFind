using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class TenderRequest : Request
    {
        public ICollection<string> Images { get; set; }
        public string QueryId { get; set; }
        public double TenderAmount { get; set; }
        public string TenderDescription { get; set; }
        public string TenderId { get; set; }
        public string TenderState { get; set; }

        public TenderRequest()
        {
            Images = new List<string>();
        }
    }
}
