using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class TenderResponse : Response
    {
              
        public TenderResponse(string errorMessage):base(errorMessage){}
        public TenderResponse() { }

        public SalerDTO SalerDTO { get; set; }
        public ICollection<TenderDTO> Tenders { get; set; }
        public TenderDTO TenderDTO { get; set; }
    }
}
