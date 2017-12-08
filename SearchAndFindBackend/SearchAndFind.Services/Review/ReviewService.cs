using SearchAndFind.Core;
using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class ReviewService : IReviewService
    {
        private IReviewManager reviewManager;
        private IRequestValidator<ReviewRequest> requestValidator;
        private AuthenticationController authenticationController;
        private ICloudMessageSender messageSender;
        private AbstractUserManager<ClientRequest> clientManager;
        private AbstractUserManager<SalerRequest> salerManager;

        public ReviewService(IReviewManager manager,  ICloudMessageSender mSender, AbstractUserManager<ClientRequest> clientMan, AbstractUserManager<SalerRequest> salerMan)
        {
            reviewManager = manager;
            requestValidator = new ReviewRequestValidator();
            authenticationController = new AuthenticationController(salerMan, clientMan);
            messageSender = mSender;
            clientManager = clientMan;
            salerManager = salerMan;
        }

        public Response addReviewFromClient(ReviewRequest request)
        {
            try
            {
                authenticationController.CheckClientAuthentication(new ClientRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
                requestValidator.ValidateRequest(request);
                ReviewDTO dto=reviewManager.AddReviewFromClient(request);
                NotifyDestination(dto,salerManager.GetUserDTOById(dto.DestinationId.ToString()),"reviewFromClient");
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
                return new Response("Ocurrió un error al intentar registar su review");
            }
        }

        private void NotifyDestination(ReviewDTO reviewDTO, UserDTO user, string notificationTag)
        {
            messageSender.SendMessage(BuildReviewCloudMessage(reviewDTO,user, notificationTag));
        }

        private CloudMessage BuildReviewCloudMessage(ReviewDTO reviewDTO,UserDTO user, string notificationTag)
        {
            CloudMessage message = new CloudMessage();
            message.to = user.DeviceId;
            message.notification = BuildReviewMessageNotification(reviewDTO);
            Dictionary<string, string> messageData = new Dictionary<string, string>();
            messageData.Add(CloudUtil.FUNCTION_TAG, notificationTag);
            message.data = messageData;
            return message;
        }

        private Message BuildReviewMessageNotification(ReviewDTO reviewDTO)
        {
            Message message = new Message();
            message.title = "Nueva Calificación!";
            message.body = "Puntación: " + reviewDTO.Points;
            return message;
        }

        public Response addReviewFromSaler(ReviewRequest request)
        {
            try
            {
                authenticationController.CheckSalerAuthentication(new SalerRequest() { AuthenticationType = request.AuthenticationType, UserId = request.UserId, CurrentToken = request.CurrentToken });
                requestValidator.ValidateRequest(request);
                ReviewDTO dto = reviewManager.AddReviewFromSaler(request);
                NotifyDestination(dto,clientManager.GetUserDTOById(dto.DestinationId.ToString()),"reviewFromSaler");
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
                return new Response("Ocurrió un error al intentar registar su review");
            }
        }
    }
}
