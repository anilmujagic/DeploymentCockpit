using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Common;

namespace DeploymentCockpit.Models
{
    public partial class Variable
    {
        public VariableScope Scope
        {
            get { return this.ScopeKey.ToEnum<VariableScope>(); }
            set { this.ScopeKey = value.ToString(); }
        }
    }
}
