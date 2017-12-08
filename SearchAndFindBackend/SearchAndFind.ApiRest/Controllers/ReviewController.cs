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
    public class ReviewController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IReviewService reviewService;

        public ReviewController(IReviewService service)
        {
            reviewService = service;
        }

        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/reviews/client")]
        public IHttpActionResult ReviewFromClient(ReviewRequest request)
        {
            try
            {
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = reviewService.addReviewFromClient(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on refiewFromClient: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }


        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/reviews/saler")]
        public IHttpActionResult ReviewFromSaler(ReviewRequest request)
        {
            try
            {
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = reviewService.addReviewFromSaler(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error on refiewFromSaler: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }
    }
}
