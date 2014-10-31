using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insula.Common;

namespace DeploymentCockpit.ApiDtos
{
    public class VariablesHierarchyInfoDto
    {
        public string ScopeKey { get; private set; }
        public int ScopeID { get; private set; }
        public string ScopeName { get; private set; }
        public List<VariableDto> Variables { get; private set; }
        public List<VariablesHierarchyInfoDto> Children { get; private set; }

        public VariablesHierarchyInfoDto(VariableScope scope, int scopeID, string scopeName)
        {
            this.ScopeKey = scope.GetName();
            this.ScopeID = scopeID;
            this.ScopeName = scopeName;
            this.Variables = new List<VariableDto>();
            this.Children = new List<VariablesHierarchyInfoDto>();
        }
    }
}
