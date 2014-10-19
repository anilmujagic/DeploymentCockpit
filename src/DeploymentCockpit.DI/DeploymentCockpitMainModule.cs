using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DeploymentCockpit.Common;
using DeploymentCockpit.Data;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.ScriptExecution;
using DeploymentCockpit.Services;

namespace DeploymentCockpit.DI
{
    public class DeploymentCockpitMainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Single Instance

            builder.RegisterType<UnitOfWorkFactory>()
                .As<IUnitOfWorkFactory>()
                .InstancePerLifetimeScope();

            #endregion


            #region Data

            var dataAssembly = typeof(UnitOfWork).Assembly;

            builder.RegisterAssemblyTypes(dataAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(dataAssembly)
                .Where(t => t.Name.Contains("UnitOfWork"))
                .AsImplementedInterfaces();

            #endregion


            #region Business Logic

            var businessLogicAssembly = typeof(ProjectService).Assembly;

            builder.RegisterAssemblyTypes(businessLogicAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            #endregion
        }
    }
}
