using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class CategoryResponse:Response
    {
        public ICollection<CategoryDTO> Categories { get; set; }

        public CategoryResponse(string message) : base(message) { }

        public CategoryResponse()
        {
            Categories = new List<CategoryDTO>();
        }
    }
}
