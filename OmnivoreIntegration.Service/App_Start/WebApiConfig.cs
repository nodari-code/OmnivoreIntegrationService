using System.Net;
using System.Web.Http;

using OmnivoreIntegration.ExceptionHandling;

namespace OmnivoreIntegration.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            if (config == null) throw new InternalServerErrorException("config is null.");

            //Add Exception filter
            config.Filters.Add(new ApiExceptionFilterAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
