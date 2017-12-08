using SearchAndFind.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using SearchAndFind.DTO;
using System.Web.Http.Description;
using log4net;

namespace SearchAndFind.ApiRest.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClientController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IClientService clientService;

        public ClientController(IClientService service)
        {
            clientService = service;
        }

        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/clients/signin")]
        public IHttpActionResult SignIn(ClientRequest request)
        {
            try
            {
                Response requestResponse = clientService.SignIn(request);
                return Ok(requestResponse);                                              
            }
            catch (Exception e)
            {
                logger.Error("Error signIn client", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");                
                return Ok(errorResponse);                
            }
        }

        [ResponseType(typeof(Response))]
        [HttpPost]
        [Route("api/clients/signup")]
        public IHttpActionResult SignUp(ClientRequest request)
        {
            try
            {
                Response requestResponse = clientService.SignUp(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error signup client", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }

    }
}
