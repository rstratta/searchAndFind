using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class SalerResponse : Response
    {
        public SalerDTO SalerDTO { get; set; }
        public ICollection<SalerCategoryDTO> SalerCategoryDTO { get; set; }
        public SalerResponse()
        {
            SalerCategoryDTO = new List<SalerCategoryDTO>();
        }
    }
}
