using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.ApiDtos
{
    public class ProjectVersionInfo
    {
        public string ProjectName { get; set; }
        public string EnvironmentName { get; set; }
        public int? DeploymentJobID { get; set; }
        public string ProductVersion { get; set; }
        public string DeploymentJobTime { get; set; }
    }
}
