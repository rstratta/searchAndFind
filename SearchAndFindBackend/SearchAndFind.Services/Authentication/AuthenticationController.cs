using SearchAndFind.Core;
using SearchAndFind.DTO;
using SearchAndFind.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class AuthenticationController
    {
        public static readonly string SEARCH_AND_FIND_AUTHENTICATION = "syf";
        public static readonly string GOOGLE_AUTHENTICATION = "google";
        private Dictionary<string, IAuthenticationChecker> strategyAuthenticationChecker;

        public AuthenticationController(AbstractUserManager<SalerRequest> salerMan, AbstractUserManager<ClientRequest> clientMan)
        {
            strategyAuthenticationChecker = new Dictionary<string, IAuthenticationChecker>();
            IAuthenticationChecker searchAndFindChecker = new SearchAndFindAuthenticationChecker(salerMan, clientMan);
            strategyAuthenticationChecker.Add(SEARCH_AND_FIND_AUTHENTICATION, searchAndFindChecker);
            strategyAuthenticationChecker.Add(GOOGLE_AUTHENTICATION, new GoogleAuthenticationChecker(searchAndFindChecker));
        }
       
        public void CheckClientAuthentication(UserRequest request)
        {
            ValidateRequest(request);
            IAuthenticationChecker checker = GetCheckerFromAuthenticationType(request.AuthenticationType);
            checker.CheckClientAuthentication(request);
        }

        private IAuthenticationChecker GetCheckerFromAuthenticationType(string authenticationType)
        {
            if (!strategyAuthenticationChecker.ContainsKey(authenticationType))
            {
                throw new ServiceOperationException("Error en canal de autenticación");
            }
            return strategyAuthenticationChecker[authenticationType];
        }

        public void CheckSalerAuthentication(UserRequest request)
        {
            ValidateRequest(request);
            IAuthenticationChecker checker = GetCheckerFromAuthenticationType(request.AuthenticationType);
            checker.CheckSalerAuthentication(request);
        }

        private void ValidateRequest(UserRequest request)
        {
            if (request.CurrentToken == null)
            {
                throw new AuthenticationException("Error al recibir token de autenticación. Favor vuelva a autenticarse");
            }
        }

        public void CheckBothProfileAuthentication(UserRequest request)
        {
            ValidateRequest(request);
            IAuthenticationChecker checker = GetCheckerFromAuthenticationType(request.AuthenticationType);
            checker.CheckBothProfileAuthentication(request);
        }
    }
}
