using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.ApiDtos
{
    public class ProjectDto
    {
        public short ProjectID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "API Code cannot be longer than 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9_-]{0,100}$",
            ErrorMessage = "API Code can contain only characters, numbers, underscores (_) and dashes (-).")]
        public string ApiCode { get; set; }

        public string Description { get; set; }
    }
}
