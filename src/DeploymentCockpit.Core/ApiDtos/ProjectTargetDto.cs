using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.ApiDtos
{
    public class ProjectTargetDto
    {
        public int ProjectTargetID { get; set; }

        [Required(ErrorMessage = "The Target Group field is required.")]
        public short? TargetGroupID { get; set; }
        public string TargetGroupName { get; set; }

        [Required(ErrorMessage = "The Environment field is required.")]
        public short? ProjectEnvironmentID { get; set; }
        public string ProjectEnvironmentName { get; set; }

        [Required(ErrorMessage = "The Target field is required.")]
        public short? TargetID { get; set; }
        public string TargetName { get; set; }
    }
}
