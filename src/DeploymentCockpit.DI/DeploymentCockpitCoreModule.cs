using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DeploymentCockpit.Common;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.ScriptExecution;

namespace DeploymentCockpit.DI
{
    // This class is linked in Target project. Be carefull when changing to avoid introducing dependencies.
    public class DeploymentCockpitCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Ambient Contexts

            //Log.LogTarget = new SomeLogTarget();
            DomainContext.ConfigurationProvider = new AppSettingsConfigurationProvider();

            #endregion

            builder.RegisterType<TargetCommandSender>().As<ITargetCommandSender>();
            builder.RegisterType<TargetCommandProcessor>().As<ITargetCommandProcessor>();
            builder.RegisterType<ScriptRunner>().As<IScriptRunner>();
        }
    }
}
