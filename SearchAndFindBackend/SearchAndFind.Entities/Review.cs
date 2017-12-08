using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Points { get; set; }
        public Guid OrigUserId { get; set; }
        public Guid DestinationUserId { get; set; }

        public Review()
        {
            Id = Guid.NewGuid();
            ReviewDate = DateTime.Now;
        }

        public Review(Guid oUserId, Guid destnUserId,  int points) : this()
        {
            OrigUserId = oUserId;
            DestinationUserId = destnUserId;
            Points = points;
        }
        public override bool Equals(object obj)
        {
            return Id.Equals(((Review)obj).Id);
        }


     }
 }
