using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

using OmnivoreIntegration.ExceptionHandling;

namespace OmnivoreIntegration.Service
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
            }
            catch (InternalServerErrorException aex)
            {
                Console.WriteLine(aex.Message);
            }
        }
    }
}
