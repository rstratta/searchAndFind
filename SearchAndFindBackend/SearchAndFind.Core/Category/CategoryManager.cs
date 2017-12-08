using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAndFind.Entities;
using SearchAndFind.DTO;

namespace SearchAndFind.Core
{
    public class CategoryManager : ICategoryManager
    {
        private ICategoryRepository categoryRepository;
        private IDTOBuilder<CategoryDTO, Category> categoryBuilderDTO;

        public CategoryManager(ICategoryRepository repository, IDTOBuilder<CategoryDTO, Category> dtoBuilder)
        {
            categoryRepository = repository;
            categoryBuilderDTO = dtoBuilder;
        }
        public Response GetAll()
        {
            try
            {
                ICollection<Category> categories = categoryRepository.GetAll();
                return BuildResponse(categories);
            }
            catch(RepositoryException e)
            {
                throw new ManagerException(e.Message);
            }
        }

        private Response BuildResponse(ICollection<Category> categories)
        {
            CategoryResponse response = new CategoryResponse();
            response.Categories= BuildCategoryDTOList(categories);
            return response;
        }

        private ICollection<CategoryDTO> BuildCategoryDTOList(ICollection<Category> categories)
        {
            ICollection<CategoryDTO> result = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                result.Add(categoryBuilderDTO.BuildDTO(category));
            }
            return result;
        }
    }
}
