using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Entities
{
    public abstract class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MailAddress { get; set; }
        public string DeviceId { get; set; }
        public string Password { get; set; }
        public bool Eliminated { get; set; }
        public string CurrentToken { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public int NumberOfReview { get; set; }
        public double AverageReview { get; set; }

        public override bool Equals(object obj)
        {
            return Id.Equals(((User)obj).Id);
        }
    }
}
