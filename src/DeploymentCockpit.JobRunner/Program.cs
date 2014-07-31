using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DeploymentCockpit.Common;
using DeploymentCockpit.DI;
using DeploymentCockpit.Interfaces;
using Insula.Common;
using Topshelf;

namespace DeploymentCockpit.JobRunner
{
    class Program
    {
        private static IContainer _diContainer;

        private static void Main(string[] args)
        {
            AutoMapperConfiguration.Configure();
            _diContainer = InitDI();

            HostFactory.Run(h =>
                {
                    h.Service<MainService>(s =>
                    {
                        s.ConstructUsing(() => new MainService(_diContainer.Resolve<IDeploymentJobExecutionService>()));
                        s.WhenStarted(p => p.Start());
                        s.WhenStopped(p => p.Stop());
                        s.WhenPaused(p => p.Stop());
                        s.WhenContinued(p => p.Start());
                        s.WhenShutdown(p => p.Stop());
                    });

                    h.SetServiceName("DeploymentCockpitJobRunner");
                    h.SetDisplayName("Deployment Cockpit Job Runner");
                    h.SetDescription("Executes jobs from Deployment Cockpit server");

                    h.StartAutomatically();
                    h.RunAsLocalSystem();
                    h.EnableServiceRecovery(s =>
                    {
                        s.RestartService(1); // On 1st failure restart the service after 1 minute
                        s.RestartService(1); // On 2nd failure restart the service after 1 minute
                        s.RestartService(1); // On 3rd and subsequent failures restart the service after 1 minute
                        s.SetResetPeriod(1); // Set the counter reset interval to one day
                    });
                });
        }

        private static IContainer InitDI()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DeploymentCockpitMainModule>();
            return builder.Build();
        }
    }
}
