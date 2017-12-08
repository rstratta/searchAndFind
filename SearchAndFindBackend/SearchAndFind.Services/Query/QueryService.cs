using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAndFind.DTO;
using SearchAndFind.Core;
using System.Configuration;

namespace SearchAndFind.Services
{
    public class QueryService : IQueryService
    {
        private readonly int EMPTY_LIST = 0;
        private IQueryManager queryManager;
        private IRequestValidator<QueryRequest> requestValidator;
        private ISalerManager salerManager;
        private AuthenticationController authenticationController;
        private ICloudMessageSender messageSender;
        private ITenderManager tenderManager;
        public QueryService(IQueryManager queryMan, ICloudMessageSender cloudMessageSender, ISalerManager salerMan, AbstractUserManager<SalerRequest> absSalerManager, AbstractUserManager<ClientRequest> clientManager, ITenderManager tenderMan)
        {
            queryManager = queryMan;
            requestValidator = new QueryRequestValidator();
            salerManager = salerMan;
            authenticationController = new AuthenticationController(absSalerManager, clientManager);
            messageSender = cloudMessageSender;
            tenderManager = tenderMan;
        }

        public Response DoQuery(QueryRequest request)
        {
            try
            {
                authenticationController.CheckClientAuthentication(new ClientRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
                requestValidator.ValidateRequest(request);
                QueryDTO queryDTO = queryManager.DoQuery(request);
                ICollection<SalerAvailableForTenderDTO> salers = GetSalersToNotify(queryDTO);
                NotifySalers(salers,queryDTO);
                queryDTO.Salers = salers;
                return new QueryResponse() { QueryDTO = queryDTO };
            }
            catch (BadRequestException e)
            {
                return new Response(e.Message);
            }
            catch (ManagerException e)
            {
                return new Response(e.Message);
            }
            catch (RepositoryException e)
            {
                return new Response(e.Message);
            }
            catch (ServiceOperationException e)
            {
                CancelQuery(request);
                return new Response(e.Message);
            }
            catch (AuthenticationException e)
            {
                Response response = new Response(e.Message);
                response.AuthenticationError = true;
                return response;
            }
            catch (Exception)
            {
                return new Response("Ocurrió un error al intentar registar su consulta");
            }
        }



        public Response GetPendingQuery(QueryRequest request)
        {
            try
            {
                authenticationController.CheckClientAuthentication(new ClientRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
                requestValidator.ValidateMandatoryFields(request);
                QueryDTO query = queryManager.GetCurrentQuery(request);
                return new QueryResponse() { QueryDTO = query };
            }
            catch (BadRequestException e)
            {
                return new Response(e.Message);
            }
            catch (ManagerException e)
            {
                return new Response(e.Message);
            }
            catch (RepositoryException e)
            {
                return new Response(e.Message);
            }
            catch (AuthenticationException e)
            {
                Response response = new Response(e.Message);
                response.AuthenticationError = true;
                return response;
            }
            catch (Exception)
            {
                return new Response("Ocurrió un error al obtener su consulta pendiente");
            }
        }

        public Response CancelQuery(QueryRequest request)
        {
            try
            {
                authenticationController.CheckClientAuthentication(new ClientRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
                requestValidator.ValidateMandatoryFields(request);
                queryManager.CancelQuery(request);
                return new Response();
            }
            catch (BadRequestException e)
            {
                return new Response(e.Message);
            }
            catch (ManagerException e)
            {
                return new Response(e.Message);
            }
            catch (RepositoryException e)
            {
                return new Response(e.Message);
            }
            catch (AuthenticationException e)
            {
                Response response = new Response(e.Message);
                response.AuthenticationError = true;
                return response;
            }
            catch (Exception)
            {
                return new Response("Ocurrió un error al cancelar su consulta");
            }
        }
        public Response ConfirmQuery(QueryRequest request)
        {
            try
            {
                authenticationController.CheckClientAuthentication(new ClientRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
                new ConfirmQueryRequestValidator().ValidateMandatoryFields(request);
                queryManager.ConfirmQuery(request);
                TenderResponse tenderResponse = tenderManager.GetTenderById(request.TenderConfirmId);
                CloudMessage message = BuildConfirmCloudMessage(tenderResponse.SalerDTO, request.TenderConfirmId);
                messageSender.SendMessage(message);
                return new Response();
            }
            catch (BadRequestException e)
            {
                return new Response(e.Message);
            }
            catch (ManagerException e)
            {
                return new Response(e.Message);
            }
            catch (RepositoryException e)
            {
                return new Response(e.Message);
            }
            catch (AuthenticationException e)
            {
                Response response = new Response(e.Message);
                response.AuthenticationError = true;
                return response;
            }
            catch (Exception)
            {
                return new Response("Ocurrió un error al confirmar su consulta");
            }
        }

        private CloudMessage BuildConfirmCloudMessage(SalerDTO salerDTO,string tenderId)
        {
            CloudMessage message = new CloudMessage();
            message.to =  salerDTO.DeviceId ;
            Dictionary<string, string> messageData = new Dictionary<string, string>();
            messageData.Add(CloudUtil.FUNCTION_TAG, "query");
            messageData.Add(CloudUtil.TOKEN_ID, tenderId);
            message.data = messageData;
            return message;
        }

        private ICollection<SalerAvailableForTenderDTO> GetSalersToNotify(QueryDTO queryDTO)
        {
            ICollection<SalerAvailableForTenderDTO> salersToNotify = salerManager.GetSalersNearQueryLocalization(queryDTO.Latitude, queryDTO.Length, queryDTO.QueryDate, queryDTO.CategoryId);
            if (salersToNotify.Count == EMPTY_LIST)
            {
                throw new ServiceOperationException("No se obtuvieron locales cercanos que cumplan el criterio de búsqueda");
            }
            return salersToNotify;

        }

        private void NotifySalers(ICollection<SalerAvailableForTenderDTO> salers, QueryDTO queryDTO)
        {
            foreach (var saler in salers)
            {
                messageSender.SendMessage(BuildQueryCloudMessage(saler, queryDTO));
            }
            
        }

        private CloudMessage BuildQueryCloudMessage(SalerAvailableForTenderDTO saler, QueryDTO queryDTO)
        {
            CloudMessage message = new CloudMessage();
            message.to = saler.DeviceId;
            message.notification= BuildQueryMessageNotification(queryDTO);
            Dictionary<string, string> messageData = new Dictionary<string, string>();
            messageData.Add(CloudUtil.FUNCTION_TAG, "query");
            messageData.Add(CloudUtil.TOKEN_ID, queryDTO.Id.ToString());
            messageData.Add(CloudUtil.CATEGORY_NAME, queryDTO.CategoryName);
            messageData.Add(CloudUtil.CATEGORY_DESCRIPTION, queryDTO.Description);
            message.data = messageData;
            return message;
        }

        private Message BuildQueryMessageNotification(QueryDTO queryDTO)
        {
            Message message = new Message();
            message.title = "Nueva búsqueda de cliente!";
            message.body = "Categoría: " + queryDTO.CategoryName + " Descripción del pedido:" + queryDTO.Description;
            return message;
        }

        private string[] GetDestination(ICollection<SalerAvailableForTenderDTO> salers)
        {
            string[] destination = new string[salers.Count];
            int index = 0;
            foreach (var saler in salers)
            {
                destination[index] = saler.DeviceId;
                index++;
            }
            return destination;
        }

        public Response GetQueryById(QueryRequest request)
        {
            try
            {
                authenticationController.CheckSalerAuthentication(new SalerRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
                QueryDTO queryDTO = queryManager.GetQueryById(request.QueryId);
                return new QueryResponse() { QueryDTO = queryDTO };
            }
            catch (BadRequestException e)
            {
                return new Response(e.Message);
            }
            catch (ManagerException e)
            {
                return new Response(e.Message);
            }
            catch (RepositoryException e)
            {
                return new Response(e.Message);
            }
            catch (ServiceOperationException e)
            {
                CancelQuery(request);
                return new Response(e.Message);
            }
            catch (AuthenticationException e)
            {
                Response response = new Response(e.Message);
                response.AuthenticationError = true;
                return response;
            }
            catch (Exception)
            {
                return new Response("Ocurrió un error al intentar obtener su consulta");
            }
        }
    }
}
