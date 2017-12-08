using SearchAndFind.DTO;
using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public class TenderDTOBuilder : IDTOBuilder<TenderDTO, Tender>
    {
        

        public TenderDTOBuilder()
        {

        }
        public TenderDTO BuildDTO(Tender entity)
        {
            TenderDTO dto = new TenderDTO();
            dto.TenderId=entity.Id.ToString();
            dto.TenderAmount = entity.Amount;
            dto.Description = entity.Description;
            dto.QueryId = entity.QueryId.ToString();
            dto.Images = BuildImagesFromEntity(entity);
            dto.ReviewFromClient = entity.ClientReviewId.ToString();
            dto.ReviewFromSaler = entity.SalerReviewId.ToString();
            dto.State = entity.State;
            return dto;
        }

        private ICollection<string> BuildImagesFromEntity(Tender entity)
        {
            ICollection<string> imageResult = new List<string>();
            foreach (var image in entity.Images)
            {
                imageResult.Add(image.EncodedImage);
            }
            return imageResult;
        }
    }
}
