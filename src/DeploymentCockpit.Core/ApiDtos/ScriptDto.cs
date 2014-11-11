using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.ApiDtos
{
    public class ScriptDto
    {
        public short ScriptID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Script Type cannot be longer than 50 characters.")]
        public string ScriptType { get; set; }

        public string Body { get; set; }
        public string SuccessKeywords { get; set; }
        public string FailureKeywords { get; set; }

        public short? ProjectID { get; set; }

        public string ProjectName { get; set; }
    }

    public class ScriptDtoForList
    {
        public short ScriptID { get; set; }
        public string Name { get; set; }
        public string ScriptType { get; set; }
        public string ProjectName { get; set; }
    }
}
