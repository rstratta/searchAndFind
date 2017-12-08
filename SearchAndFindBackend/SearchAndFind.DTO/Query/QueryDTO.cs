using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class QueryDTO
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime QueryDate { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public double Latitude { get; set; }
        public double Length { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<SalerAvailableForTenderDTO> Salers { get; set; }
        public string ClientId { get; set; }

        public QueryDTO() { }
    }
}
