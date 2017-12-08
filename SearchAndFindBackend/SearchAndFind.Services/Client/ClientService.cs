using System;
using SearchAndFind.DTO;
using SearchAndFind.Core;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SearchAndFind.Services
{
    public class ClientService : AbstractUserService<ClientRequest>, IClientService
    {
       
        private IReviewManager reviewManager;
        private ITenderManager tenderManager;
        private IQueryManager queryManager;        

        public ClientService(AbstractUserManager<ClientRequest> clientManager, AbstractUserManager<SalerRequest> salerManager,
            IReviewManager reviewMan, ITenderManager tenderMan,  IQueryManager queryMan)
        {
            userManager = clientManager;
            reviewManager = reviewMan;
            tenderManager = tenderMan;
            queryManager = queryMan;
            authenticationController = new AuthenticationController(salerManager, clientManager);
        }

       

        protected override void ValidateMandatoryFields(ClientRequest request)
        {
            IRequestValidator<ClientRequest> validator = new ClientRequestValidator();
            validator.ValidateMandatoryFields(request);
        }
    }
}
