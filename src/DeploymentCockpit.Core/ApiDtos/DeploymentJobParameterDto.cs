using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.ApiDtos
{
    public class DeploymentJobParameterDto
    {
        public int DeploymentJobParameterID { get; set; }
        public short ProjectID { get; set; }
        public string Name { get; set; }
    }
}
