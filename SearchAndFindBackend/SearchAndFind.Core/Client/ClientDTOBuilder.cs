using SearchAndFind.DTO;
using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public class ClientDTOBuilder :IDTOBuilder<ClientDTO, Client>
    {
        public ClientDTOBuilder()
        {
        }
        public ClientDTO BuildDTO(Client entity)
        {
            ClientDTO dto = new ClientDTO(entity.Id, entity.Name, entity.LastName, entity.MailAddress, entity.DeviceId);
            dto.CurrentToken = entity.CurrentToken;
            dto.AverageReview = entity.AverageReview;
            dto.NumberOfReview = entity.NumberOfReview;
            return dto;
        }
    }
}
