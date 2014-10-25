using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Common;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;
using Insula.Common;
using Newtonsoft.Json;
using ZeroMQ;

namespace DeploymentCockpit.ScriptExecution
{
    public class TargetCommandProcessor : ITargetCommandProcessor
    {
        private readonly IScriptRunner _scriptRunner;

        public TargetCommandProcessor(IScriptRunner scriptRunner)
        {
            if (scriptRunner == null)
                throw new ArgumentNullException("scriptRunner");
            _scriptRunner = scriptRunner;
        }

        public void ProcessCommand()
        {
            var ipAddress = DomainContext.IPAddress;
            if (ipAddress.IsNullOrWhiteSpace())
                ipAddress = "*";  // Bind to all interfaces
            var portNumber = DomainContext.PortNumber;
            var endpoint = "tcp://{0}:{1}".FormatString(ipAddress, portNumber);

            var encriptionKey = DomainContext.TargetKey;
            var encriptionSalt = DomainContext.ServerKey;

            using (var context = ZmqContext.Create())
            {
                using (var socket = context.CreateSocket(SocketType.REP))
                {
                    Log.Info("{0}Listening...", Environment.NewLine);
                    socket.Bind(endpoint);
                    var encryptedJson = socket.Receive(Encoding.UTF8);
                    Log.Info("Command received");
                    var json = EncryptionHelper.Decrypt(encryptedJson, encriptionKey, encriptionSalt);

                    var command = JsonConvert.DeserializeObject<ScriptExecutionCommand>(json);
                    // if (command.CommandTime.AddSeconds(60) < DateTime.UtcNow)
                    //     throw new TimeoutException();  // To prevent re-execution of script by using sniffed packet.

                    Log.Info("Executing {0} script...", command.ScriptType);
                    var output = _scriptRunner.Run(command.ScriptBody, command.ScriptType.ToEnum<ScriptType>());
                    Log.Success("Script executed");

                    Log.Info("Sending results...");
                    var encryptedOutput = EncryptionHelper.Encrypt(output, encriptionKey, encriptionSalt);
                    socket.Send(encryptedOutput, Encoding.UTF8);
                    Log.Success("Results sent");
                }
            }
        }
    }
}
