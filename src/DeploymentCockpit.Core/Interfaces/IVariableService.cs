using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface IVariableService : ICrudService<Variable>
    {
        IEnumerable<TDto> GetAllForScopeAs<TDto>(VariableScope scope, int scopeID);
        IEnumerable<TDto> GetAllForScopeAs<TDto>(string scopeKey, int scopeID);
        string ResolveVariables(
            Script script, DeploymentPlanStep planStep, DeploymentJob job, string environmentName,
            short? targetGroupID = null, int? targetGroupEnvironmentID = null, int? projectTargetID = null,
            string targetComputerName = null, string credentialUsername = null, string credentialPassword = null);
    }
}
