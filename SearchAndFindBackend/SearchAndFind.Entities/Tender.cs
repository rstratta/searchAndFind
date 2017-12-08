using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Entities
{
    public class Tender
    {
        public static string PENDING_TENDER = "P";
        public static string REVOKE_TENDER = "R";
        public static string ACEPT_TENDER = "A";
        public Guid Id { get; set; }
        public Guid SalerId { get; set; }
        public Guid ClientId { get; set; }
        public virtual ICollection<TenderImage> Images { get; set; }
        public Guid QueryId { get; set; }
        public DateTime TenderDate { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string State { get; set; }
        public Guid ClientReviewId { get; set; }
        public Guid SalerReviewId { get; set; }

        public Tender()
        {
            Id = Guid.NewGuid();
            TenderDate = DateTime.Now;
            State = PENDING_TENDER;
            Images = new List<TenderImage>();
        }

        public Tender(Guid queryId, Guid salerId, string description, double amount) : this()
        {
            QueryId = queryId;
            SalerId = salerId;
            Description = description;
            Amount = amount;
        }


        public override bool Equals(object obj)
        {
            return Id.Equals(((Tender)obj).Id);
        }
    }
}
