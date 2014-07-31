using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.ApiDtos
{
    public class VariableDto
    {
        public int VariableID { get; set; }

        [Required]
        public string ScopeKey { get; set; }
        [Required]
        public int ScopeID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
