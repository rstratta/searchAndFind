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
    public class QueryController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IQueryService queryService;

        public QueryController(IQueryService service)
        {
            queryService = service;
        }

        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/queries/doIt")]
        public IHttpActionResult DoQuery(QueryRequest request)
        {
            try
            {
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = queryService.DoQuery(request);
                return Ok(requestResponse);
            }
            catch (Exception)
            {
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

        [ResponseType(typeof(Response))]
        [HttpGet]
        [Route("api/queries/{id}/pendingQuery")]
        public IHttpActionResult PendingQuery(string id)
        {
            try
            {
                QueryRequest request= new QueryRequest();
                request.UserId = id;
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = queryService.GetPendingQuery(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on pendingQuery: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/queries/cancel")]
        public IHttpActionResult CancelQuery(QueryRequest request)
        {
            try
            {
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = queryService.CancelQuery(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on cancelQuery: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/queries/confirm")]
        public IHttpActionResult ConfirmQuery(QueryRequest request)
        {
            try
            {
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = queryService.ConfirmQuery(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on confirmQuery: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }
        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/queries/queryId")]
        public IHttpActionResult GetQueryById(QueryRequest request)
        {
            try
            {
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = queryService.GetQueryById(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on  getQueryById ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

    }
}
