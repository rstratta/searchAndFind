using SearchAndFind.DTO;
using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public class ReviewDTOBuilder : IDTOBuilder<ReviewDTO, Review>
    {
        public ReviewDTO BuildDTO(Review entity)
        {
            ReviewDTO dto = new ReviewDTO();
            dto.Id = entity.Id;
            dto.Points = entity.Points;
            dto.ReviewDate = entity.ReviewDate;
            dto.DestinationId = entity.DestinationUserId;
            return dto;
        }
    }
}
