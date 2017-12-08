using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class TenderDTO
    {
        public string Description { get; set; }
        public ICollection<string> Images{ get; set; }
        public double PointsFromClient { get; set; }
        public double PointsFromSaler { get; set; }
        public string QueryId { get; set; }
        public string ReviewFromClient { get; set; }
        public string ReviewFromSaler { get; set; }
        public FullSalerDTO SalerDTO { get; set; }
        public string State { get; set; }
        public double TenderAmount { get; set; }
        public string TenderId { get; set; }
    }
}
