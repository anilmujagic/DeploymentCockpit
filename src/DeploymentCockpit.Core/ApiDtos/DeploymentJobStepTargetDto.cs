using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Common;
using Insula.Common;

namespace DeploymentCockpit.ApiDtos
{
    public class DeploymentJobStepTargetDto : ExecutionInfo
    {
        public int DeploymentJobStepTargetID { get; set; }
        public string TargetName { get; set; }
        public int DeploymentJobStepID { get; set; }
    }
}
