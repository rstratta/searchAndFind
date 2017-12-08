using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class ReviewDTO
    {
        public Guid Id { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Points { get; set; }
        public object DestinationId { get; set; }

        public ReviewDTO() { }
    }
}
