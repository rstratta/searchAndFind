using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAndFind.DTO;
using SearchAndFind.Core;

namespace SearchAndFind.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryManager categoryManager;
        private AuthenticationController authenticationController;

        public CategoryService(ICategoryManager manager, AbstractUserManager<SalerRequest> salerManager, AbstractUserManager<ClientRequest> clientManager)
        {
            categoryManager = manager;
            authenticationController = new AuthenticationController(salerManager, clientManager);
        }
        public Response GetCategories(UserRequest request)
        {

            try
            {
                authenticationController.CheckBothProfileAuthentication(request);
                return categoryManager.GetAll();
            }
            catch (BadRequestException e)
            {
                return new Response(e.Message);
            }
            catch (AuthenticationException e)
            {
                Response response = new Response(e.Message);
                response.AuthenticationError = true;
                return response;
            }
            catch (ManagerException e)
            {
                return new Response(e.Message);
            }
            catch (RepositoryException e)
            {
                return new Response(e.Message);
            }
        }
    }
}
