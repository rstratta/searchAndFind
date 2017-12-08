using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class BoundDTO
    {
        public double MinLatitude { get; set; }
        public double MaxLatitude { get; set; }
        public double MinLength { get; set; }
        public double MaxLength { get; set; }
        public double EarthRadio { get; set; }
    }
}
