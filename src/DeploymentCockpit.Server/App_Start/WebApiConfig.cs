using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DeploymentCockpit.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                "DashboardApi",
                "api/Dashboard/{action}/{id}",
                new { id = RouteParameter.Optional }
            );

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
#if DEBUG
            // When debugging (e.g. with Fiddler or Postman) we want to get JSON response by default.
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
#endif
        }
    }
}
