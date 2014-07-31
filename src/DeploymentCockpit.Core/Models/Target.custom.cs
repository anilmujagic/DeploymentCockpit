using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insula.Common;

namespace DeploymentCockpit.Models
{
    public partial class Target
    {
        public string UsernameWithDomain
        {
            get
            {
                var username = this.Credential.Username;

                if (this.Credential.Domain.IsNullOrWhiteSpace())
                    username = this.ComputerName + "\\" + username;
                else
                    username = this.Credential.Domain + "\\" + username;

                return username;
            }
        }
    }
}
