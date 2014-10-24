using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Common;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;
using Insula.Common;

namespace DeploymentCockpit.Services
{
    public class ScriptExecutionService : IScriptExecutionService
    {
        private readonly ITargetService _targetService;
        private readonly ICredentialService _credentialService;
        private readonly ITargetCommandSender _targetCommandSender;
        private readonly IScriptRunner _scriptRunner;

        public ScriptExecutionService(
            ITargetService targetService,
            ICredentialService credentialService,
            IVariableService variableService,
            ITargetCommandSender targetCommandSender,
            IScriptRunner scriptRunner)
        {
            if (targetService == null)
                throw new ArgumentNullException("targetService");
            _targetService = targetService;

            if (credentialService == null)
                throw new ArgumentNullException("credentialService");
            _credentialService = credentialService;

            if (targetCommandSender == null)
                throw new ArgumentNullException("targetCommandSender");
            _targetCommandSender = targetCommandSender;

            if (scriptRunner == null)
                throw new ArgumentNullException("scriptRunner");
            _scriptRunner = scriptRunner;
        }

        public ScriptExecutionResult ExecuteScript(ScriptJobDescriptor descriptor)
        {
            var result = new ScriptExecutionResult();

            try
            {
                var scriptType = descriptor.ScriptType.ToEnum<ScriptType>();
                var scriptBody = descriptor.ScriptBody;

                if (descriptor.RemoteExecution)
                {
                    if (!descriptor.TargetID.HasValue)
                        throw new Exception("Target not specified.");
                    var target = _targetService.GetByKey(descriptor.TargetID.Value);

                    result.Output = this.ExecuteScriptRemotely(scriptBody, scriptType,
                        target.ComputerName, target.PortNumber, target.TargetKey);
                }
                else
                {
                    result.Output = this.ExecuteScriptLocally(scriptBody, scriptType);
                }

                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);

                foreach (var error in ex.GetExceptionTreeAsFlatList())
                {
                    result.Output += error.Message + Environment.NewLine;
                    result.Output += error.StackTrace;
                }
            }

            return result;
        }

        private string ExecuteScriptLocally(string scriptBody, ScriptType scriptType)
        {
            return _scriptRunner.Run(scriptBody, scriptType);
        }

        private string ExecuteScriptRemotely(string scriptBody, ScriptType scriptType,
            string computerName, int portNumber, string targetKey)
        {
            var command = new ScriptExecutionCommand
                {
                    ScriptBody = scriptBody,
                    ScriptType = scriptType.GetName(),
                    CommandTime = DateTime.UtcNow
                };

            var encriptionKey = targetKey;
            var encriptionSalt = DomainContext.ServerKey;

            return _targetCommandSender.Send(computerName, portNumber, command, encriptionKey, encriptionSalt);
        }
    }
}
