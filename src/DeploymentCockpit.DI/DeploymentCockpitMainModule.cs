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
            #region Ambient Contexts

            //Log.LogTarget = new SomeLogTarget();
            DomainContext.ConfigurationProvider = new AppSettingsConfigurationProvider();

            #endregion


            #region Single Instance

            builder.RegisterType<UnitOfWorkFactory>()
                .As<IUnitOfWorkFactory>()
                .InstancePerLifetimeScope();

            #endregion


            #region Business Logic

            var businessLogicAssembly = typeof(ProjectService).Assembly;

            builder.RegisterAssemblyTypes(businessLogicAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

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


            builder.RegisterType<TargetCommandSender>().As<ITargetCommandSender>();
            builder.RegisterType<TargetCommandProcessor>().As<ITargetCommandProcessor>();
            builder.RegisterType<ScriptRunner>().As<IScriptRunner>();
        }
    }
}
