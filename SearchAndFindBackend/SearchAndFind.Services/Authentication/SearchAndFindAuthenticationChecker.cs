using SearchAndFind.Core;
using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class SearchAndFindAuthenticationChecker : IAuthenticationChecker 
    {
        private AbstractUserManager<SalerRequest> salerManager;
        private AbstractUserManager<ClientRequest> clientManager;

        public SearchAndFindAuthenticationChecker(AbstractUserManager<SalerRequest> salerMan, AbstractUserManager<ClientRequest> clientMan)
        {
            salerManager = salerMan;
            clientManager = clientMan;
        }
      

        public void CheckSalerAuthentication(UserRequest request)
        {
            UserDTO userDTO = salerManager.GetUserDTOById(request.UserId);            
            ValidateDTO(userDTO, request);
        }

        private void ValidateDTO(UserDTO userDTO, UserRequest request)
        {
            if (userDTO.CurrentToken==null || !userDTO.CurrentToken.Equals(request.CurrentToken))
            {
                throw new AuthenticationException("Debe autenticarse para realizar esta operación");
            }
        }

        public void CheckClientAuthentication(UserRequest request)
        {
            UserDTO userDTO = clientManager.GetUserDTOById(request.UserId);
            ValidateDTO(userDTO, request);
        }

       

        public void CheckBothProfileAuthentication(UserRequest request)
        {
            try
            {
                CheckSalerAuthentication(request);

            }
            catch (RepositoryException)
            {
                CheckClientAuthentication(request);
            }
            catch (ManagerException)
            {
                CheckClientAuthentication(request);
            }
            catch (Exception e)
            {
                throw new AuthenticationException(e.Message);
            }
        }

      
    }
}
