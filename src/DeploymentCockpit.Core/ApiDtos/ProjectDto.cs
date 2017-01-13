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
        public string ApiCode { get; set; }

        public string Description { get; set; }
    }
}
