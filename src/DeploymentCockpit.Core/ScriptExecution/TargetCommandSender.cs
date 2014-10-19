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
    public class TargetCommandSender : ITargetCommandSender
    {
        public string Send(string computerName, int portNumber, ScriptExecutionCommand command,
            string encriptionKey, string encriptionSalt)
        {
            var endpoint = "tcp://{0}:{1}".FormatString(computerName, portNumber);
            var json = JsonConvert.SerializeObject(command);
            var encryptedJson = EncryptionHelper.Encrypt(json, encriptionKey, encriptionSalt);

            using (var context = ZmqContext.Create())
            {
                using (var socket = context.CreateSocket(SocketType.REQ))
                {
                    socket.Connect(endpoint);
                    socket.Send(encryptedJson, Encoding.UTF8);

                    var encryptedReply = socket.Receive(Encoding.UTF8);
                    var reply = EncryptionHelper.Decrypt(encryptedReply, encriptionKey, encriptionSalt);

                    return reply;
                }
            }
        }
    }
}
