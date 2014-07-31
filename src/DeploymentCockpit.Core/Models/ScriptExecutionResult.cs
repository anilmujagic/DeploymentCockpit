using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.Models
{
    public class ScriptExecutionResult
    {
        public bool IsSuccessful { get; set; }
        public string Output { get; set; }
    }
}
