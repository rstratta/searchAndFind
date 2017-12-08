using SearchAndFind.DTO;
using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public class FullSalerDTOBuilder : IDTOBuilder<FullSalerDTO, Saler>
    {
  
        public FullSalerDTO BuildDTO(Saler entity)
        {
            FullSalerDTO salerDTO = new FullSalerDTO(entity.Id, entity.Name, entity.LastName, entity.MailAddress, entity.DeviceId);
            salerDTO.Latitude = entity.Latitude;
            salerDTO.Length = entity.Length;
            salerDTO.ShopAddress = entity.ShopAddress;
            salerDTO.ShopDaysOpen = entity.ShopDaysOpen;
            salerDTO.ShopHourOpen = entity.ShopHourOpen;
            salerDTO.ShopHourClose = entity.ShopHourClose;
            salerDTO.ShopName = entity.ShopName;
             salerDTO.ShopPhone = entity.ShopPhone;
            salerDTO.CurrentToken = entity.CurrentToken;
            salerDTO.AverageReview = entity.AverageReview;
            salerDTO.NumberOfReview = entity.NumberOfReview;
            return salerDTO;
        }

       
    }
}
