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
    public class DeploymentJobStepDto : ExecutionInfo
    {
        public int DeploymentJobStepID { get; set; }
        public string DeploymentPlanStepName { get; set; }
        public IEnumerable<DeploymentJobStepTargetDto> Targets { get; set; }
    }
}
