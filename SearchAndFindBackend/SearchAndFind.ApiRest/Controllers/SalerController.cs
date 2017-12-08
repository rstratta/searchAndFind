using log4net;
using SearchAndFind.DTO;
using SearchAndFind.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace SearchAndFind.ApiRest.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SalerController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ISalerService salerService;

        
        public SalerController(ISalerService service)
        {
            salerService = service;
        }

        [ResponseType(typeof(Response))]
        [HttpGet]
        [Route("api/salers/{id}/saler")]
        public IHttpActionResult SalerById(string id)
        {
            try
            {
                SalerRequest request = new SalerRequest();
                request.UserId = id;
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = salerService.GetUserById(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on salerById: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/salers/signin")]
        public IHttpActionResult SignIn(SalerRequest request)
        {
            try
            {
                Response requestResponse = salerService.SignIn(request);                
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on salerSignIn: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/salers/signup")]
        public IHttpActionResult SignUp(SalerRequest request)
        {
            try
            {
                    Response requestResponse = salerService.SignUp(request);                
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on salerSignUp: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }
      

        [ResponseType(typeof(Response))]
        [HttpGet]
        [Route("api/salers/{id}/avaibleCategories")]
        public IHttpActionResult GetAvailableCategories(string id)
        {
            try
            {
                SalerRequest request = new SalerRequest();
                request.UserId = id;
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = salerService.GetAvailableCategories(request);                
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on salerAvailableCategories: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

        [ResponseType(typeof(Response))]
        [HttpPut]
        [Route("api/salers/salerCategory")]
        public IHttpActionResult PutSalerCategories([FromBody] SalerRequest request)
        {
            try
            {
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = salerService.UpdateCategoriesFromSeler(request);
                return Ok(requestResponse);
            }
            catch (Exception)
            {
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/salers/updateAccount")]
        public IHttpActionResult UpdateAccount(SalerRequest request)
        {
            try
            {
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = salerService.UpdateAccount(request);
                return Ok(requestResponse);
            }
            catch (Exception)
            {
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }
    }
}
