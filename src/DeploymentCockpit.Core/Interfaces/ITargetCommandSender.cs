using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface ITargetCommandSender
    {
        string Send(string computerName, int portNumber, ScriptExecutionCommand command,
            string encriptionKey, string encriptionSalt);
    }
}
