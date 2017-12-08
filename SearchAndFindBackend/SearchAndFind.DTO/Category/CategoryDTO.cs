using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class CategoryDTO
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public override bool Equals(object obj)
        {
            return Id.Equals(((CategoryDTO)obj).Id);
        }
    }
}
