using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.ApiDtos
{
    public class DeploymentJobStepFlatDto : ExecutionInfo
    {
        public int DeploymentJobStepID { get; set; }
        public string DeploymentPlanStepName { get; set; }
        public int DeploymentJobStepTargetID { get; set; }
        public string TargetName { get; set; }
    }
}
