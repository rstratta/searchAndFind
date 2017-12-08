using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class ClientDTO : UserDTO
    {
        /*public ICollection<ReviewDTO> Reviews;
        public ICollection<QueryDTO> Queries;*/

        

        public ClientDTO(Guid Id, string name, string lastName, string mailAddress, string deviceId):base(Id, name, lastName, mailAddress, deviceId)
        { 
            /*Reviews = new List<ReviewDTO>();
            Queries = new List<QueryDTO>();*/
        }
    }
}
