using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Common;

namespace DeploymentCockpit.Models
{
    public partial class DeploymentJobStepTarget
    {
        public DeploymentStatus Status
        {
            get { return this.StatusKey.ToEnum<DeploymentStatus>(DeploymentStatus.Queued); }
            set { this.StatusKey = value.ToString(); }
        }
    }
}
