using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Entities
{
    public class Query
    {
        public static string PENDING_STATE = "P";
        public static string CONFIRMED_STATE = "X";
        public static string CANCELED_STATE = "C";
            public Guid Id { get; set; }
            public virtual Client Client { get; set; }
            public Guid ClientId { get; set; }
            public virtual Category Category { get; set; }
            public Guid CategoryId { get; set; }
            public DateTime QueryDate { get; set; }
            public string Description { get; set; }
            public double ClientLatitude { get; set; }
            public double ClientLength { get; set; }
            public string State { get; set; }
            public int Retry { get; set; }


        public Query()
        {
            Id = Guid.NewGuid();
            State = PENDING_STATE;
            QueryDate = DateTime.Now;
        }

        public Query(Guid clientId, Guid categoryId, string description, long latitude, long length):this()
        {
            ClientId = clientId;
            CategoryId = categoryId;
            Description = description;
            ClientLatitude = latitude;
            ClientLength = length;
        }
        public override bool Equals(object obj)
        {
            return Id.Equals(((Query)obj).Id);
        }
    }
}
