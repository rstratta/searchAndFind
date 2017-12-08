using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class QueryRequest:Request
    {
        public string Category { get; set; }       
        public string Descritpion { get; set; }
        public double Latitude { get; set; }
        public double Length { get; set; }
        public string QueryId { get; set; }
        public string TenderConfirmId { get; set; }
        
    }
}
