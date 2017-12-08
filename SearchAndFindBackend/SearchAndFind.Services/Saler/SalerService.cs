using System;
using SearchAndFind.DTO;
using SearchAndFind.Core;
using System.Collections.Generic;

namespace SearchAndFind.Services
{
    public class SalerService : AbstractUserService<SalerRequest>, ISalerService
    {
        private IReviewManager reviewManager;
        private ITenderManager tenderManager;
       private ISalerManager salerManager;

        public SalerService(AbstractUserManager<SalerRequest> userMan,
            IReviewManager reviewMan, ITenderManager tenderMan, ISalerManager salerMan,  AbstractUserManager<ClientRequest> clientManager)
        {
            userManager = userMan;
            reviewManager = reviewMan;
            tenderManager = tenderMan;
            salerManager = salerMan;
            authenticationController = new AuthenticationController(userMan, clientManager);
        }

        public Response GetAvailableCategories(SalerRequest request)
        {
            try
            {
                authenticationController.CheckSalerAuthentication(request);
                IRequestValidator<SalerRequest> validator = new SalerCategoryRequestValidtor();
                validator.ValidateRequest(request);
                ICollection<SalerCategoryDTO> salerCategoriesDTO = salerManager.GetCategoriesSaler(request);
                SalerResponse response = new SalerResponse();
                response.SalerCategoryDTO = salerCategoriesDTO;
                return response;

            }
            catch (BadRequestException e)
            {
                return new Response(e.Message);
            }
            catch(AuthenticationException e){
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
        public Response UpdateCategoriesFromSeler(SalerRequest salerRequest)
        {
            try
            {
                Guid idSaler = Guid.Parse(salerRequest.UserId);
                authenticationController.CheckSalerAuthentication(salerRequest);
                IRequestValidator<SalerRequest> validator = new SalerCategoryRequestValidtor();
                validator.ValidateRequest(salerRequest);
                salerManager.UpdateCategoriesFromSaler(idSaler, salerRequest);
                return new SalerResponse();
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

        public Response UpdateAccount(SalerRequest request)
        {
            authenticationController.CheckSalerAuthentication(new SalerRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
            IRequestValidator<SalerRequest> validator = new SalerRequestValidator();
            validator.ValidateMandatoryFields(request);
            return userManager.UpdateAccount(request);
        }

        protected override void ValidateMandatoryFields(SalerRequest request)
        {
            IRequestValidator<SalerRequest> validator = new SalerRequestValidator();
            validator.ValidateMandatoryFields(request);

        }
    }
}
