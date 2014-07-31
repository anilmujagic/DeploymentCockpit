using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;
using Insula.Common;

namespace DeploymentCockpit.ScriptExecution
{
    public class ScriptRunner : IScriptRunner
    {
        public string Run(string scriptBody, ScriptType scriptType)
        {
            var scriptPath = string.Empty;

            try
            {
                scriptPath = this.CreateScriptFile(scriptBody, scriptType);
                var command = this.CreateCommand(scriptPath, scriptType);
                return this.ExecuteCommand(command);
            }
            finally
            {
                if (File.Exists(scriptPath))
                    File.Delete(scriptPath);
            }
        }

        private string CreateScriptFile(string scriptBody, ScriptType scriptType)
        {
            var fileExtension = this.GetScriptExtension(scriptType);
            var fileName = "DeploymentCockpit_{0:N}{1}".FormatString(Guid.NewGuid(), fileExtension);
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            File.WriteAllText(filePath, scriptBody);

            return filePath;
        }

        private string GetScriptExtension(ScriptType scriptType)
        {
            switch (scriptType)
            {
                case ScriptType.Batch:
                    return ".bat";
                case ScriptType.PowerShell:
                    return ".ps1";
                default:
                    throw new Exception("Unknown ScriptType [{0}].".FormatString(scriptType.GetName()));
            }
        }

        private string CreateCommand(string scriptPath, ScriptType scriptType)
        {
            if (scriptType == ScriptType.PowerShell)
            {
                return "PowerShell -NoProfile -ExecutionPolicy Bypass -Command \"& '{0}'\"".FormatString(scriptPath);
            }
            else  // Batch
            {
                return scriptPath;
            }
        }

        private string ExecuteCommand(string command)
        {
            var outputFile = this.GetRandomOutputFileName();

            try
            {
                var arguments = "/c {0} > \"{1}\" 2>&1".FormatString(command, outputFile);

                var p = new Process();
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = "cmd";
                p.StartInfo.Arguments = arguments;
                p.Start();

                p.WaitForExit();

                var output = File.ReadAllText(outputFile);
                return output;
            }
            finally
            {
                if (File.Exists(outputFile))
                    File.Delete(outputFile);
            }
        }

        private string GetRandomOutputFileName()
        {
            var fileName = "DeploymentCockpit_ScriptOutput_{0:N}.txt".FormatString(Guid.NewGuid());
            return Path.Combine(Path.GetTempPath(), fileName);
        }
    }
}
