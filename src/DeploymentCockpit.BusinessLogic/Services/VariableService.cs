using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Common;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;
using Insula.Common;

namespace DeploymentCockpit.Services
{
    public class VariableService : CrudService<Variable>, IVariableService
    {
        public VariableService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IEnumerable<TDto> GetAllForScopeAs<TDto>(string scopeKey, int scopeID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<Variable>()
                    .GetAllAs<TDto>(v => v.ScopeKey == scopeKey && v.ScopeID == scopeID);
            }
        }

        public string ResolveVariables(Script script, DeploymentPlanStep planStep, DeploymentJob job,
            short? targetGroupID = null, string targetComputerName = null,
            string credentialUsername = null, string credentialPassword = null)
        {
            List<Variable> variables;

            using (var uow = _unitOfWorkFactory.Create())
            {
                variables = uow.Repository<Variable>()
                    .GetAll(v =>
                        v.ScopeKey == VariableScope.Global.ToString()
                        || (v.ScopeKey == VariableScope.Project.ToString() && v.ScopeID == job.ProjectID)
                        || (v.ScopeKey == VariableScope.TargetGroup.ToString() && v.ScopeID == targetGroupID)
                        || (v.ScopeKey == VariableScope.Environment.ToString() && v.ScopeID == job.ProjectEnvironmentID)
                        || (v.ScopeKey == VariableScope.DeploymentPlan.ToString() && v.ScopeID == planStep.DeploymentPlanID)
                        || (v.ScopeKey == VariableScope.DeploymentStep.ToString() && v.ScopeID == planStep.DeploymentPlanStepID))
                    .OrderByDescending(v => v.Scope)  // First enum value (Global) has lowest precedence.
                    .ToList();
            }

            var scriptBody = script.Body;

            foreach (var parameter in script.Parameters)
            {
                var variable = variables
                    .FirstOrDefault(v => v.Name == parameter.Name && !v.Value.IsNullOrWhiteSpace());

                string value = null;

                if (variable != null)
                    value = variable.Value;

                if (value.IsNullOrWhiteSpace())
                    value = this.GetValueFromPredefinedVariable(parameter.Name, job.ProductVersion,
                        targetComputerName, credentialUsername, credentialPassword);

                if (value.IsNullOrWhiteSpace())
                    value = parameter.DefaultValue;

                if (value.IsNullOrWhiteSpace())
                {
                    if (parameter.IsMandatory)
                        throw new Exception(
                            "Variables did not provide value for mandatory parameter [{0}] in script [{1}]."
                                .FormatString(parameter.Name, script.Name));

                    continue;
                }
                else
                {
                    var placeholder = VariableHelper.FormatPlaceholder(parameter.Name);
                    if (scriptBody.Contains(placeholder))
                        scriptBody = scriptBody.Replace(placeholder, value);
                }
            }

            return scriptBody;
        }

        private string GetValueFromPredefinedVariable(string parameterName, string productVersion,
            string targetComputerName = null, string credentialUsername = null, string credentialPassword = null)
        {
            if (parameterName == VariableHelper.ProductVersionVariable)
                return productVersion;

            if (parameterName == VariableHelper.TargetComputerNameVariable)
                return targetComputerName;

            if (parameterName == VariableHelper.CredentialUsernameVariable)
                return credentialUsername;
            if (parameterName == VariableHelper.CredentialPasswordVariable)
                return credentialPassword;

            return null;
        }
    }
}
