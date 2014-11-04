using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.ApiDtos
{
    public class DeploymentPlanParameterDto
    {
        public int DeploymentPlanParameterID { get; set; }
        public short DeploymentPlanID { get; set; }
        public string Name { get; set; }
    }
}
