using SearchAndFind.Core;
using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public abstract class AbstractUserService<R> where R : UserRequest
    {
        protected AbstractUserManager<R> userManager;
        protected AuthenticationController authenticationController;


        public Response SignIn(R request)
        {
            try
            {
                return DecideAndDoSignInByAuthenticator(request);
                
            }
            catch (RepositoryException e)
            {
                return new Response(e.Message);
            }
            catch (ManagerException e)
            {
                return new Response(e.Message);
            }
            catch (BadRequestException e)
            {
                return new Response(e.Message);
            }
        }

        private Response DecideAndDoSignInByAuthenticator(R request)
        {
            if (request.AuthenticationType.Equals(AuthenticationController.GOOGLE_AUTHENTICATION))
            {
                return DoGoogleSignIng(request); 
            }else
            {
                return DoInternalSignIn(request);
            }
        }

        private Response DoGoogleSignIng(R request)
        {
            return userManager.SignInByGoogleAuthentication(request);
        }

        private Response DoInternalSignIn(R request)
        {
            IRequestValidator<UserRequest> loginRequestValidator = new LoginRequestValidator();
            loginRequestValidator.ValidateRequest(request);
            return userManager.SignIn(request);
        }

        public Response SignUp(R request)
        {
            
            try
            {
                IRequestValidator<UserRequest> loginRequestValidator = new LoginRequestValidator();
                loginRequestValidator.ValidateRequest(request);
                return userManager.SignUp(request);
            }
            catch (RepositoryException e)
            {
                return new Response(e.Message);
            }
            catch (ManagerException e)
            {
                return new Response(e.Message);
            }
            catch (BadRequestException e)
            {
                return new Response(e.Message);
            }
        }

        public Response GetUserById(R request)
        {
            try
            {
                authenticationController.CheckBothProfileAuthentication(request);
                IRequestValidator<UserRequest> loginRequestValidator = new UserIdValidator();
                loginRequestValidator.ValidateRequest(request);
                return userManager.GetUserById(request);
            }
            catch (RepositoryException e)
            {
                return new Response(e.Message);
            }
            catch (ManagerException e)
            {
                return new Response(e.Message);
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
        }
        protected abstract void ValidateMandatoryFields(R request);
    }
}
