using SearchAndFind.DTO;
using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public class UserDTOBuilder : IDTOBuilder<UserDTO, User>
    {
        public UserDTO BuildDTO(User entity)
        {
            return new UserDTO() { Id = entity.Id, CurrentToken = entity.CurrentToken, DeviceId=entity.DeviceId };
        }
    }
}
