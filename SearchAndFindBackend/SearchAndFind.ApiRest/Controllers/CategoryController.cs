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
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace SearchAndFind.ApiRest.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoryController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ICategoryService categoryService;

        public CategoryController(ICategoryService service)
        {
            categoryService = service;
        }

        [ResponseType(typeof(Response))]
        [HttpGet]
        [Route("api/categories/{id}/all")]
        public IHttpActionResult GetAll(string id)
        {
            try
            {
                UserRequest request = new UserRequest();
                request.UserId = id;
                request.CurrentToken = ControllerHelper.GetTokenFromHttpRequest(Request);
                request.AuthenticationType = ControllerHelper.GetAuthenticationTypeFromHttpRequest(Request);
                Response requestResponse = categoryService.GetCategories(request);
                return Ok(requestResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error: ", e);
                Response errorResponse = new Response("Error inesperado al procesar la solicitud, intente nuevamente mas tarde");
                return Ok(errorResponse);
            }
        }
    }
}
