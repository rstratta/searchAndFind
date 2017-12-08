using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public class SalerAvailablesToTenderQueryFilter
    {
        public double QueryLatitude { get; set; }
        public double QueryLength { get; set; }
        public double MaxLatitude { get; set; }
        public double MinLatitude { get; set; }
        public double MaxLength { get; set; }
        public double MinLength { get; set; }
        public double EarthRadius { get; set; }
        public Guid CategoryId { get; set; }
        public int HourQuery { get; set; }
        public string DayOfQuery { get; set; }
        public double Distance { get; set; }
    }
}
