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

        public IEnumerable<TDto> GetAllForScopeAs<TDto>(VariableScope scope, int scopeID)
        {
            var scopeKey = scope.GetName();
            return this.GetAllForScopeAs<TDto>(scopeKey, scopeID);
        }

        public IEnumerable<TDto> GetAllForScopeAs<TDto>(string scopeKey, int scopeID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<Variable>()
                    .GetAs<TDto>(v => v.ScopeKey == scopeKey && v.ScopeID == scopeID);
            }
        }

        public string ResolveVariables(
            Script script, DeploymentPlanStep planStep, DeploymentJob job, string environmentName,
            short? targetGroupID = null, int? targetGroupEnvironmentID = null, int? projectTargetID = null,
            string targetComputerName = null, string credentialUsername = null, string credentialPassword = null)
        {
            if (script.Body.IsNullOrWhiteSpace())
                throw new ArgumentException("Script body is empty.");

            List<Variable> variables;
            using (var uow = _unitOfWorkFactory.Create())
            {
                variables = uow.Repository<Variable>()
                    .Get(v =>
                        v.ScopeKey == VariableScope.Global.ToString()
                        || (v.ScopeKey == VariableScope.Project.ToString()
                            && v.ScopeID == job.ProjectID)
                        || (v.ScopeKey == VariableScope.TargetGroup.ToString()
                            && v.ScopeID == targetGroupID)
                        || (v.ScopeKey == VariableScope.Environment.ToString()
                            && v.ScopeID == job.ProjectEnvironmentID)
                        || (v.ScopeKey == VariableScope.TargetGroupEnvironment.ToString()
                            && v.ScopeID == targetGroupEnvironmentID)
                        || (v.ScopeKey == VariableScope.ProjectTarget.ToString()
                            && v.ScopeID == projectTargetID)
                        || (v.ScopeKey == VariableScope.DeploymentPlan.ToString()
                            && v.ScopeID == planStep.DeploymentPlanID)
                        || (v.ScopeKey == VariableScope.DeploymentStep.ToString()
                            && v.ScopeID == planStep.DeploymentPlanStepID)
                        || (v.ScopeKey == VariableScope.DeploymentJob.ToString()
                            && v.ScopeID == job.DeploymentJobID))
                    .OrderByDescending(v => v.Scope)  // First enum value (Global) has lowest precedence.
                    .ToList();
            }

            var parameters = script.Parameters
                .OrderBy(v => v.Name)
                .ToList();
            parameters.Add(new ScriptParameter { Name = VariableHelper.DeploymentJobNumberVariable });
            parameters.Add(new ScriptParameter { Name = VariableHelper.ProductVersionVariable });
            parameters.Add(new ScriptParameter { Name = VariableHelper.EnvironmentNameVariable });
            parameters.Add(new ScriptParameter { Name = VariableHelper.TargetComputerNameVariable });
            parameters.Add(new ScriptParameter { Name = VariableHelper.CredentialUsernameVariable });
            parameters.Add(new ScriptParameter { Name = VariableHelper.CredentialPasswordVariable });

            // Replace variable placeholders with values in multiple passes
            // to enable using variables inside other variables.
            var cyclesLeft = 10;
            var originalBody = script.Body;
            while (true)
            {
                var processedBody = this.ReplacePlaceholders(originalBody, script.Name, parameters, variables,
                    job, environmentName, targetComputerName, credentialUsername, credentialPassword);

                if (processedBody == originalBody)  // Nothing left to replace
                    return processedBody;

                originalBody = processedBody;

                if (--cyclesLeft == 0)
                    throw new Exception("Possible cyclic reference in variables.");
            }
        }

        private string ReplacePlaceholders(string scriptBody, string scriptName,
            IEnumerable<ScriptParameter> parameters, IEnumerable<Variable> variables,
            DeploymentJob job, string environmentName,
            string targetComputerName = null, string credentialUsername = null, string credentialPassword = null)
        {
            foreach (var parameter in parameters)
            {
                var variable = variables
                    .FirstOrDefault(v => v.Name == parameter.Name && !v.Value.IsNullOrWhiteSpace());

                string value = null;

                if (variable != null)
                    value = variable.Value;

                if (value.IsNullOrWhiteSpace())
                    value = this.GetValueFromPredefinedVariable(
                        parameter.Name, job.DeploymentJobID, job.ProductVersion, environmentName,
                        targetComputerName, credentialUsername, credentialPassword);

                if (value.IsNullOrWhiteSpace())
                    value = parameter.DefaultValue;

                if (value.IsNullOrWhiteSpace() && parameter.IsMandatory)
                {
                    throw new Exception(
                        "Variables did not provide value for mandatory parameter [{0}] in script [{1}]."
                            .FormatString(parameter.Name, scriptName));
                }

                var placeholder = VariableHelper.FormatPlaceholder(parameter.Name);
                if (scriptBody.Contains(placeholder))
                    scriptBody = scriptBody.Replace(placeholder, value);
            }

            return scriptBody;
        }

        private string GetValueFromPredefinedVariable(
            string parameterName, int deploymentJobID, string productVersion, string environmentName,
            string targetComputerName = null, string credentialUsername = null, string credentialPassword = null)
        {
            if (parameterName == VariableHelper.DeploymentJobNumberVariable)
                return deploymentJobID.ToString("D");

            if (parameterName == VariableHelper.ProductVersionVariable)
                return productVersion;

            if (parameterName == VariableHelper.EnvironmentNameVariable)
                return environmentName;

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
