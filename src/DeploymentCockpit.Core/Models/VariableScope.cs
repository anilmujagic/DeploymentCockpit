using System;

/// <summary>
/// Order of enum values represents inverse order of precedence for variables.
/// </summary>
public enum VariableScope
{
    Global,
    Project,
    TargetGroup,
    Environment,
    DeploymentPlan,
    DeploymentStep
}
