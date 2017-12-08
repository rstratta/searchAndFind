using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
    
        

        public Category() {
            Id = Guid.NewGuid();
        }

        public Category(string name, string description):this()
        {
            Name = name;
            Description = description;
        }
        public override bool Equals(object obj)
        {
            return Id.Equals(((Category)obj).Id);
        }
    }
}
