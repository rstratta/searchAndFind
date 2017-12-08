using SearchAndFind.DTO;
using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public class QueryDTOBuilder : IDTOBuilder<QueryDTO, Query>
    {
        public QueryDTO BuildDTO(Query entity)
        {
            QueryDTO dto = new QueryDTO();
            dto.CategoryName = entity.Category.Name;
            dto.Description = entity.Description;
            dto.QueryDate = entity.QueryDate;
            dto.State = entity.State;
            dto.Id = entity.Id;
            dto.Latitude = entity.ClientLatitude;
            dto.Length = entity.ClientLength;
            dto.CategoryId = entity.CategoryId;
            dto.ClientId = entity.ClientId.ToString();
            return dto;
        }
    }
}
