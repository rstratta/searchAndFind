using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAndFind.DTO;
using SearchAndFind.Core;

namespace SearchAndFind.Services
{
    public class TenderService : ITenderService
    {
        private AbstractUserManager<SalerRequest> salerManager;
        private AbstractUserManager<ClientRequest> clientManager;
        private ITenderManager tenderManager;
        private IQueryManager queryManager;
        private IRequestValidator<TenderRequest> requestValidator;
        private ICloudMessageSender messageSender;
        private AuthenticationController authenticationController;

        public TenderService(AbstractUserManager<SalerRequest> salerMan, AbstractUserManager<ClientRequest> clientMan,
            ITenderManager tenderMan, IQueryManager queryMan, ICloudMessageSender mSender)
        {
            salerManager = salerMan;
            clientManager = clientMan;
            tenderManager = tenderMan;
            requestValidator = new TenderRequestValidator();
            queryManager = queryMan;
            authenticationController = new AuthenticationController(salerManager, clientManager);
            messageSender = mSender;

        }
        public Response DoTender(TenderRequest request)
        {
            try
            {
                authenticationController.CheckSalerAuthentication(new SalerRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
                requestValidator.ValidateMandatoryFields(request);
                TenderDTO tenderDTO = tenderManager.DoTender(request);
                QueryDTO queryDTO = queryManager.GetQueryById(tenderDTO.QueryId);
                UserDTO user = clientManager.GetUserDTOById(queryDTO.ClientId);
                NotifyClient(user, tenderDTO);
                return new TenderResponse() { TenderDTO = tenderDTO };
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
                return new Response("Ocurrió un error al intentar registar su oferta");
            }
        }

        private void NotifyClient(UserDTO user, TenderDTO tenderDTO)
        {
            messageSender.SendMessage(BuildTenderCloudMessage(user, tenderDTO));
        }

        private CloudMessage BuildTenderCloudMessage(UserDTO user, TenderDTO tenderDTO)
        {
            CloudMessage message = new CloudMessage();
            message.to = user.DeviceId;
            message.notification = BuildTenderMessageNotification(tenderDTO);
            Dictionary<string, string> messageData = new Dictionary<string, string>();
            messageData.Add(CloudUtil.FUNCTION_TAG, "tenderFromSaler");
            messageData.Add(CloudUtil.TOKEN_ID, tenderDTO.TenderId);
            messageData.Add(CloudUtil.QUERY_ID, tenderDTO.QueryId);
            messageData.Add(CloudUtil.SALER_LATITUDE, tenderDTO.SalerDTO.Latitude.ToString());
            messageData.Add(CloudUtil.SALER_LENGTH, tenderDTO.SalerDTO.Length.ToString());
            messageData.Add(CloudUtil.TENDER_AMOUNT, tenderDTO.TenderAmount.ToString());
            messageData.Add(CloudUtil.TENDER_DESCRIPTION, tenderDTO.Description);
            messageData.Add(CloudUtil.SHOP_NAME, tenderDTO.SalerDTO.ShopName);
            messageData.Add(CloudUtil.SHOP_PHONE, tenderDTO.SalerDTO.ShopPhone);
            messageData.Add(CloudUtil.NUMBER_OF_REVIEW, tenderDTO.SalerDTO.NumberOfReview.ToString());
            messageData.Add(CloudUtil.AVERAGE_REVIEW, tenderDTO.SalerDTO.AverageReview.ToString());
            message.data = messageData;
            return message;
        }

        private Message BuildTenderMessageNotification(TenderDTO dto)
        {
            Message message = new Message();
            message.title = "Nueva oferta de vendedor!";
            message.body = "El vendedor oferta a: $"+dto.TenderAmount;
            return message;
        }

        public Response GetAllTenderByClient(TenderRequest request)
        {
            try
            {
                authenticationController.CheckClientAuthentication(new SalerRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
                return tenderManager.GetAceptedTendersByClientId(request.UserId);
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
                return new Response("Ocurrió un error al intentar registar su oferta");
            }
        }

        public Response GetAllTendersBySaler(TenderRequest request)
        {
            try
            {
                authenticationController.CheckSalerAuthentication(new SalerRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
                return tenderManager.GetAceptedTendersBySalerId(request.UserId);
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
                return new Response("Ocurrió un error al intentar registar su oferta");
            }
        }

        public Response GetTenderById(TenderRequest request)
        {
            try
            {
                authenticationController.CheckClientAuthentication(new SalerRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
                requestValidator.ValidateOptionalFields(request);
                return  tenderManager.GetTenderById(request.TenderId);
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
                return new Response("Ocurrió un error al obtener oferta");
            }
        }

        public Response ConfirmTender(TenderRequest request)
        {
            try
            {
                authenticationController.CheckClientAuthentication(new SalerRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
                requestValidator.ValidateOptionalFields(request);
                TenderDTO dto = tenderManager.ConfirmTender(request);
                TenderResponse response = new TenderResponse() { TenderDTO = dto };
                NotifySaler(dto);
                return response;
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
                return new Response("Ocurrió un error al obtener oferta");
            }
        }

        private void NotifySaler(TenderDTO dto)
        {
            messageSender.SendMessage(BuildNotifySalerCloudMessage(dto));
        }

        private CloudMessage BuildNotifySalerCloudMessage(TenderDTO dto)
        {
            CloudMessage message = new CloudMessage();
            message.to = dto.SalerDTO.DeviceId;
            Dictionary<string, string> messageData = new Dictionary<string, string>();
            messageData.Add(CloudUtil.FUNCTION_TAG, "confirmTender");
            messageData.Add(CloudUtil.TOKEN_ID, dto.TenderId);
            messageData.Add(CloudUtil.TENDER_AMOUNT, dto.TenderAmount.ToString());
            messageData.Add(CloudUtil.TENDER_DESCRIPTION, dto.Description);
            message.data = messageData;
            return message;
        }

        public Response RevokeTender(TenderRequest request)
        {
            try
            {
                authenticationController.CheckClientAuthentication(new SalerRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
                requestValidator.ValidateOptionalFields(request);
                return new TenderResponse() { TenderDTO = tenderManager.RevokeTender(request) };
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
                return new Response("Ocurrió un error al obtener oferta");
            }
        }
    }
}
