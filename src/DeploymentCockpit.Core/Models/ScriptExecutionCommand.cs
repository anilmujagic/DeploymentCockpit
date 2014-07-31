using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.Models
{
    public class ScriptExecutionCommand
    {
        public string ScriptBody { get; set; }
        public string ScriptType { get; set; }
        public DateTime CommandTime { get; set; }
    }
}
