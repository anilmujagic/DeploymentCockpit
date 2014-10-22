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
        public const string DeploymentJobNumberVariable = "DeploymentJobNumber";
        public const string ProductVersionVariable = "ProductVersion";
        public const string TargetComputerNameVariable = "TargetComputerName";
        public const string CredentialUsernameVariable = "CredentialUsername";
        public const string CredentialPasswordVariable = "CredentialPassword";

        public static string FormatPlaceholder(string variableName)
        {
            return "{0}{1}{2}".FormatString(
                DomainContext.VariablePlaceholderPrefix,
                variableName,
                DomainContext.VariablePlaceholderSuffix);
        }
    }
}
