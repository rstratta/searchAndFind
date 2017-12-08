using Microsoft.Practices.Unity;
using SearchAndFind.DependencyResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SearchAndFind.ApiRest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            
            ComponentLoader.LoadContainer(container, ".\\bin", "SearchAndFind.*.dll");

            config.DependencyResolver = new UnityResolver(container);
            config.MapHttpAttributeRoutes();
            var corsConfig = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsConfig);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
