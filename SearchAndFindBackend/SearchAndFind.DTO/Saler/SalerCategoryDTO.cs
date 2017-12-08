using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class SalerCategoryDTO 
    {
        public bool HasCategory { get; set; }
        public CategoryDTO Category { get; set; }
        public bool IsUpdated { get; set; }
    }
}
