using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class QueryResponse : Response
    {
        public QueryDTO QueryDTO { get; set; }

        public QueryResponse():base(){ }
    }
}
