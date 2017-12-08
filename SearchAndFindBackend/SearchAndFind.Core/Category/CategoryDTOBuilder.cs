using SearchAndFind.DTO;
using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public class CategoryDTOBuilder : IDTOBuilder<CategoryDTO, Category>
    {
        public CategoryDTOBuilder()
        {

        }
        public CategoryDTO BuildDTO(Category entity)
        {
            CategoryDTO dto = new CategoryDTO();
            dto.CategoryName = entity.Name;
            dto.CategoryDescription = entity.Description;
            dto.Id = entity.Id.ToString();
            return dto;
        }
    }
}
