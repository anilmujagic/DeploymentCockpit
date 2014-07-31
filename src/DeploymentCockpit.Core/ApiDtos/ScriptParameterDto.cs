using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.ApiDtos
{
    public class ScriptParameterDto
    {
        public int ScriptParameterID { get; set; }

        [Required(ErrorMessage = "The Script field is required.")]
        public short ScriptID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public string DefaultValue { get; set; }

        public bool IsMandatory { get; set; }
    }
}
