using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insula.Common;

namespace DeploymentCockpit.Common
{
    public static class VariableHelper
    {
        public const string ProductVersionVariable = "ProductVersion";
        public const string TargetComputerNameVariable = "Target.ComputerName";
        public const string CredentialUsernameVariable = "Credential.Username";
        public const string CredentialPasswordVariable = "Credential.Password";

        public static string FormatPlaceholder(string variableName)
        {
            return "{0}{1}{2}".FormatString(
                DomainContext.VariablePlaceholderPrefix,
                variableName,
                DomainContext.VariablePlaceholderSuffix);
        }
    }
}
