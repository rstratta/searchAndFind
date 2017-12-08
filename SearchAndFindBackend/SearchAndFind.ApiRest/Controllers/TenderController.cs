using log4net;
using SearchAndFind.DTO;
using SearchAndFind.Services;
using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace SearchAndFind.ApiRest.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TenderController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ITenderService tenderService;

        public TenderController(ITenderService service)
        {
            tenderService = service;
        }

        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/tenders/doIt")]
        public IHttpActionResult DoTender(TenderRequest request)
        {
            try
            {
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = tenderService.DoTender(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on doTender: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

        [ResponseType(typeof(Response))]
        [HttpGet]
        [Route("api/tenders/{id}/tenderBySaler")]
        public IHttpActionResult TenderBySaler(string id)
        {
            try
            {
                TenderRequest request = new TenderRequest();
                request.UserId = id;
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = tenderService.GetAllTendersBySaler(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on tenderBySaler: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

        [ResponseType(typeof(Response))]
        [HttpGet]
        [Route("api/tenders/{id}/tenderByClient")]
        public IHttpActionResult TenderByClient(string id)
        {
            try
            {
                TenderRequest request = new TenderRequest();
                request.UserId = id;
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = tenderService.GetAllTenderByClient(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on tenderByClient: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/tenders/cancel")]
        public IHttpActionResult RevokeTender(TenderRequest request)
        {
            try
            {
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = tenderService.RevokeTender(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on revokeTender: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/tenders/confirm")]
        public IHttpActionResult ConfirmTender(TenderRequest request)
        {
            try
            {
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = tenderService.ConfirmTender(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on confirmTender: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/tenders/tenderId")]
        public IHttpActionResult GetTenderById(TenderRequest request)
        {
            try
            {
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = tenderService.GetTenderById(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on getTenderById: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

    }
}
