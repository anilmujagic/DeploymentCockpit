using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeploymentCockpit.Common;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;
using Insula.Common;
using Newtonsoft.Json;
using NetMQ;

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

        public void ProcessCommand(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Log.Info("Commands processing cancellation is requested");
                return;
            }

            var ipAddress = DomainContext.IPAddress;
            if (ipAddress.IsNullOrWhiteSpace())
                ipAddress = "*";  // Bind to all interfaces
            var portNumber = DomainContext.PortNumber;
            var endpoint = "tcp://{0}:{1}".FormatString(ipAddress, portNumber);

            var encriptionKey = DomainContext.TargetKey;
            var encriptionSalt = DomainContext.ServerKey;

            if (cancellationToken.IsCancellationRequested)
            {
                Log.Info("Commands processing cancellation is requested");
                return;
            }

            using (var context = NetMQContext.Create())
            {
                using (var socket = context.CreateResponseSocket())
                {
                    Log.Info("{0}Listening...", Environment.NewLine);
                    socket.Bind(endpoint);

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var encryptedJson = socket.ReceiveString(Encoding.UTF8);
                        Log.Info("Command received");

                        string output;

                        try
                        {
                            Log.Info("Decrypting...");
                            var json = EncryptionHelper.Decrypt(encryptedJson, encriptionKey, encriptionSalt);

                            Log.Info("Deserializing...");
                            var command = JsonConvert.DeserializeObject<ScriptExecutionCommand>(json);
                            // if (command.CommandTime.AddSeconds(60) < DateTime.UtcNow)
                            //     throw new TimeoutException();  // To prevent re-execution of script by using sniffed packet.

                            Log.Info("Executing {0} script...", command.ScriptType);
                            output = _scriptRunner.Run(command.ScriptBody, command.ScriptType.ToEnum<ScriptType>());
                            Log.Success("Script executed");
                        }
                        catch (Exception ex)
                        {
                            Log.Exception(ex);
                            output = ex.GetAllMessagesWithStackTraces();
                        }

                        Log.Info("Sending results...");
                        var encryptedOutput = EncryptionHelper.Encrypt(output, encriptionKey, encriptionSalt);
                        socket.Send(encryptedOutput, Encoding.UTF8);
                        Log.Success("Results sent");
                    }
                }
            }
        }
    }
}
