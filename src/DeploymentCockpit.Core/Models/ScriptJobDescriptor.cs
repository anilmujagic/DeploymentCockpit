using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.Models
{
    public class ScriptJobDescriptor
    {
        [Required]
        public string ScriptBody { get; set; }

        [Required]
        public string ScriptType { get; set; }
        
        public short? TargetID { get; set; }
        public bool RemoteExecution { get; set; }

        public string SuccessKeywords { get; set; }
        public string FailureKeywords { get; set; }
        public bool SuccessKeywordsAllRequired { get; set; }
        public bool FailureKeywordsAllRequired { get; set; }
    }
}
