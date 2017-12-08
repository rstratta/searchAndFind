using SearchAndFind.DTO;
using System;
using SearchAndFind.Entities;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace SearchAndFind.Core
{
    public class SalerManager : AbstractUserManager<SalerRequest>, ISalerManager
    {
        protected ISalerRepository salerRepository;
        protected ICategoryRepository categoryRepository;
        private IDTOBuilder<FullSalerDTO, Saler> dtoBuilder;
        private IDTOBuilder<CategoryDTO, Category> dtoCategoryBuilder;


        public SalerManager(ISalerRepository repository,IUserRepository<Client> clientRepo, ICategoryRepository categoryRepo, IDTOBuilder<FullSalerDTO, Saler> builder, IDTOBuilder<CategoryDTO, Category> builderCategory)
        {
            salerRepository = repository;
            clientRepository = clientRepo;
            categoryRepository = categoryRepo;
            dtoBuilder = builder;
            dtoCategoryBuilder = builderCategory;
        }

        
        public override Response BuildSuccessResponse(User user)
        {
            SalerResponse response = new SalerResponse();
            response.SalerDTO = dtoBuilder.BuildDTO((Saler)user);
            return response;
        }

        protected override User GetUserById(string guid)
        {
            User user;
            try
            {
                user = salerRepository.GetById(BuildGuidFromRequest(guid));
            }
            catch (RepositoryException)
            {
                throw new ManagerException("Error al buscar al Saler : " + guid);
            }
            return user;
        }

        protected override void UpdateConcreteUser(User user, SalerRequest request)
        {
            UpdateConcreteSaler((Saler)user, request);
        }

        private void UpdateConcreteSaler(Saler saler, SalerRequest request)
        {
            saler.Latitude = request.Latitude;
            saler.Length = request.Length;
            saler.ShopAddress = request.ShopAddress;
            saler.ShopDaysOpen = request.ShopDaysOpen;
            saler.ShopHourClose = request.ShopHourClose;
            saler.ShopHourOpen = request.ShopHourOpen;
            saler.ShopName = request.ShopName;
            saler.ShopPhone = request.ShopPhone;
        }

        protected override void UpdateUser(User user)
        {
            
            try
            {
                salerRepository.UpdateObject((Saler)user);
            }
            catch (RepositoryException)
            {
                throw new ManagerException("Error al actualizar al Saler : " + user.Id);
            }
        }

        protected override User GetUserByMail(string mail)
        {
            User user;
            try
            {
                 user =  salerRepository.GetUserByMail(mail);
            }
            catch (RepositoryException)
            {
                throw new ManagerException("Error al buscar al mail: " + mail);
            }

            return user;
        }

        protected void AddSaler(Saler saler)
        {
            try
            {
                salerRepository.AddObject(saler);
            }
            catch (RepositoryException)
            {
                throw new ManagerException("Error al registrar vendedor " +  saler.MailAddress);
            }
            
        }

        protected override User BuildAndSaveUserFromRequest(SalerRequest request)
        {
            Saler saler = new Saler(request.DeviceId, request.Name, request.LastName, request.Mail);
            saler.Password = EncryptPassword(request.Password);
            saler.CurrentToken = GenerateLoginToken();
            AddSaler(saler);
            return saler;
        }

        protected override User BuildAndSaveUserFromGoogleRequest(SalerRequest request)
        {
            Saler saler = new Saler(request.DeviceId, request.Name, request.LastName, request.Mail);
            saler.Password = EncryptPassword(request.Password);
            saler.CurrentToken = request.CurrentToken;
            AddSaler(saler);
            return saler;
        }

        

        protected override void ValidateExistOtherProfileUser(string mail)
        {
            try { 
            User user=clientRepository.GetUserByMail(mail);
                if (user != null)
                {
                    throw new ManagerException("Existe un cliente con el correo que indica");
                }
            }catch(RepositoryException){}
        }

        public ICollection<SalerAvailableForTenderDTO> GetSalersNearQueryLocalization(double latitude, double length, DateTime queryDate, Guid categoryId)
        {
            SalerAvailablesToTenderQueryFilter filter = SalerAvailableQueryFilterBuilder.BuildFilter(latitude, length,queryDate, categoryId);
            ICollection<SalerAvailableForTender> salers = salerRepository.GetSalerNearCardinalCoord(filter);
            ICollection<SalerAvailableForTenderDTO> result = new List<SalerAvailableForTenderDTO>();
            IDTOBuilder<SalerAvailableForTenderDTO, SalerAvailableForTender> salerAvailableDtoBuilder = new SalerAvalableDTOBuilder();
            foreach (var saler in salers)
            {
                result.Add(salerAvailableDtoBuilder.BuildDTO(saler));
            }
            return result;
        }
     
        public ICollection<SalerCategoryDTO> GetCategoriesSaler(SalerRequest salerRequest)
        {
            Guid salerId = Guid.Parse(salerRequest.UserId);
            Saler saler = salerRepository.GetSalerWithCategories(salerId);
            FullSalerDTO fullSalerDTO = dtoBuilder.BuildDTO(saler);
            fillCategories(fullSalerDTO, saler);
            ICollection<SalerCategoryDTO>  salerCategories = fullSalerDTO.SalerCategoryDTO;
            ICollection<CategoryDTO> allCategories = ConvertToCatgoryDTO(categoryRepository.GetAll());
            ICollection<SalerCategoryDTO> salerCategoriesToReturn = CreateSalerCategoriesToReturn(salerCategories, allCategories);
            return salerCategoriesToReturn;
        }
        private void fillCategories(FullSalerDTO salerDTO, Saler entity)
        {
            if (entity.Categories != null)
            {
                IDTOBuilder<CategoryDTO, Category> dtoCategoryBuilder = new CategoryDTOBuilder();
                foreach (var category in entity.Categories)
                {
                    salerDTO.SalerCategoryDTO.Add(new SalerCategoryDTO() { Category = dtoCategoryBuilder.BuildDTO(category), HasCategory = true, IsUpdated = false });
                }
            }

        }
        private ICollection<CategoryDTO> ConvertToCatgoryDTO(ICollection<Category> categories)
        {
            ICollection<CategoryDTO> categoriesDTO = new List<CategoryDTO>();
            foreach(Category category in categories)
            {
                categoriesDTO.Add(dtoCategoryBuilder.BuildDTO(category));
            }
            return categoriesDTO;
        }
        private Category ConvertCategoryDTOtoCategory(CategoryDTO categoryDTO)
        {
            Category category = new Category();
            category.Id = Guid.Parse(categoryDTO.Id);
            category.Name = categoryDTO.CategoryName;
            categoryDTO.CategoryDescription = categoryDTO.CategoryDescription;
            return category;

        }
        private ICollection<SalerCategoryDTO> CreateSalerCategoriesToReturn(ICollection<SalerCategoryDTO> salerCategories, ICollection<CategoryDTO> allCategories)
        {
            ICollection<SalerCategoryDTO> salerCategoriesToReturn = new List<SalerCategoryDTO>();
            foreach (CategoryDTO category in allCategories)
            {
                bool salerHasCategory = this.SalerHasCategory(salerCategories,category);
                SalerCategoryDTO auxSalerCategoryDTO = new SalerCategoryDTO();
                auxSalerCategoryDTO.Category = category;
                auxSalerCategoryDTO.HasCategory = salerHasCategory;
                auxSalerCategoryDTO.IsUpdated = false;
                salerCategoriesToReturn.Add(auxSalerCategoryDTO);
                

            }
            return salerCategoriesToReturn;

        }

        private bool SalerHasCategory(ICollection<SalerCategoryDTO> salerCategories,CategoryDTO category)
        {
            foreach (SalerCategoryDTO salerCategoryDTO in salerCategories)
            {
                if (salerCategoryDTO.Category.Equals(category))
                {
                    return true;
                }
            }

            return false;
       }

        public void UpdateCategoriesFromSaler(Guid id, SalerRequest salerRequest)
        {
           try {
               
                foreach (var salerCategoryDTO in salerRequest.SalerCategoryDTO)
                {
                    if (salerCategoryDTO.IsUpdated && !salerCategoryDTO.HasCategory)
                    {
                        salerRepository.AddCategoryOnUser(id, ConvertCategoryDTOtoCategory(salerCategoryDTO.Category)); 
                    }
                    else if(salerCategoryDTO.IsUpdated && salerCategoryDTO.HasCategory)
                    {
                        salerRepository.RemoveCategoryFromUser(id, ConvertCategoryDTOtoCategory(salerCategoryDTO.Category));
                    }
                }
               
            }
            catch (RepositoryException  )
            {
                throw new ManagerException(" Error al actualizar categorias del vendedor "+ id);

            }
           
        }

        protected override User GetConcreteUserById(string userId)
        {
            try
            {
                return salerRepository.GetById(Guid.Parse(userId));
            }catch(RepositoryException)
            {
                throw new ManagerException("Error al obtener vendedor");
            }
        }

        protected override void ValidateUniqeUserMail(string mail)
        {
            try
            {
                User user = salerRepository.GetUserByMail(mail);
                if (user != null)
                {
                    throw new ManagerException("Existe un vendedor con el correo que indica");
                }
            }
            catch (RepositoryException)
            {

            }

        }
    }
}
