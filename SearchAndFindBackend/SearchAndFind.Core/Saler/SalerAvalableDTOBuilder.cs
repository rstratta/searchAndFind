using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public class SalerAvalableDTOBuilder : IDTOBuilder<SalerAvailableForTenderDTO, SalerAvailableForTender>
    {
        public SalerAvailableForTenderDTO BuildDTO(SalerAvailableForTender entity)
        {
            SalerAvailableForTenderDTO dto = new SalerAvailableForTenderDTO();
            dto.Distance = entity.Distance;
            dto.ShopName = entity.ShopName;
            dto.ShopPhone = entity.ShopPhone;
            dto.ShopAddress = entity.ShopAddress;
            dto.Latitude = entity.Latitude;
            dto.Length = entity.Length;
            dto.DeviceId = entity.DeviceId;
            return dto;
        }
    }
}
