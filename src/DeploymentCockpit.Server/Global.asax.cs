using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using DeploymentCockpit.Common;
using DeploymentCockpit.DI;

namespace DeploymentCockpit.Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfiguration.Configure();
            this.RegisterControllerFactory();
            GlobalConfiguration.Configure(WebApiConfig.Register);
#if DEBUG
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
#endif
        }

        private void RegisterControllerFactory()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DeploymentCockpitMainModule>();
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
