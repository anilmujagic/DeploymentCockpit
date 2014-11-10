using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;
using Insula.Common;

namespace DeploymentCockpit.Services
{
    public class ProjectService : CrudService<Project>, IProjectService
    {
        private readonly IVariableService _variableService;
        private readonly IDeploymentPlanStepService _deploymentPlanStepService;
        private readonly ITargetGroupEnvironmentService _targetGroupEnvironmentService;

        public ProjectService(IUnitOfWorkFactory unitOfWorkFactory,
            ITargetGroupEnvironmentService targetGroupEnvironmentService,
            IVariableService variableService,
            IDeploymentPlanStepService deploymentPlanStepService)
            : base(unitOfWorkFactory)
        {
            if (targetGroupEnvironmentService == null)
                throw new ArgumentNullException("targetGroupEnvironmentService");
            _targetGroupEnvironmentService = targetGroupEnvironmentService;

            if (variableService == null)
                throw new ArgumentNullException("variableService");
            _variableService = variableService;

            if (deploymentPlanStepService == null)
                throw new ArgumentNullException("deploymentPlanStepService");
            _deploymentPlanStepService = deploymentPlanStepService;
        }

        public IEnumerable<VariablesHierarchyInfoDto> GetVariablesHierarchy(short projectID)
        {
            var nodes = new List<VariablesHierarchyInfoDto>();

            // Global
            var globalNode = new VariablesHierarchyInfoDto(VariableScope.Global, 0, "Settings");
            globalNode.Variables.AddRange(this.GetVariables(VariableScope.Global, 0));
            nodes.Add(globalNode);

            // Project
            Project project;
            using (var uow = _unitOfWorkFactory.Create())
            {
                project = uow.Repository<Project>()
                    .GetAll(
                        p => p.ProjectID == projectID,
                        p => p.TargetGroups,
                        p => p.Environments,
                        p => p.DeploymentPlans)
                    .SingleOrDefault();
            }
            var projectNode = new VariablesHierarchyInfoDto(VariableScope.Project, projectID, project.Name);
            projectNode.Variables.AddRange(this.GetVariables(VariableScope.Project, project.ProjectID));
            nodes.Add(projectNode);

            // Target Groups
            foreach (var tg in project.TargetGroups.OrderBy(g => g.Name))
            {
                var tgNode = new VariablesHierarchyInfoDto(VariableScope.TargetGroup, tg.TargetGroupID, tg.Name);
                tgNode.Variables.AddRange(this.GetVariables(VariableScope.TargetGroup, tg.TargetGroupID));
                if (!tgNode.Variables.IsNullOrEmpty())
                    projectNode.Children.Add(tgNode);
            }

            // Project Environments
            foreach (var pe in project.Environments.OrderBy(e => e.Name))
            {
                var peNode = new VariablesHierarchyInfoDto(VariableScope.Environment, pe.ProjectEnvironmentID, pe.Name);
                peNode.Variables.AddRange(this.GetVariables(VariableScope.Environment, pe.ProjectEnvironmentID));
                if (!peNode.Variables.IsNullOrEmpty())
                    projectNode.Children.Add(peNode);
            }

            // Target Group Environments
            foreach (var tg in project.TargetGroups.OrderBy(g => g.Name))
            {
                foreach (var pe in project.Environments.OrderBy(e => e.Name))
                {
                    var name = "{0} / {1}".FormatString(tg.Name, pe.Name);
                    var tgeID = _targetGroupEnvironmentService.GetCombinationID(tg.TargetGroupID, pe.ProjectEnvironmentID);
                    if (!tgeID.HasValue)
                        continue;

                    var tgeNode = new VariablesHierarchyInfoDto(VariableScope.TargetGroupEnvironment, tgeID.Value, name);
                    tgeNode.Variables.AddRange(this.GetVariables(VariableScope.TargetGroupEnvironment, tgeID.Value));
                    if (!tgeNode.Variables.IsNullOrEmpty())
                        projectNode.Children.Add(tgeNode);
                }
            }

            // Deployment Plans
            foreach (var dp in project.DeploymentPlans.OrderBy(p => p.Name))
            {
                var dpNode = new VariablesHierarchyInfoDto(VariableScope.DeploymentPlan, dp.DeploymentPlanID, dp.Name);
                dpNode.Variables.AddRange(this.GetVariables(VariableScope.DeploymentPlan, dp.DeploymentPlanID));
                projectNode.Children.Add(dpNode);

                // Deployment Plan Steps
                var steps = _deploymentPlanStepService.GetAllForDeploymentPlan(dp.DeploymentPlanID);
                foreach (var ds in steps.OrderBy(s => s.ExecutionOrder))
                {
                    var dsNode = new VariablesHierarchyInfoDto(VariableScope.DeploymentStep, ds.DeploymentPlanStepID, ds.Name);
                    dsNode.Variables.AddRange(this.GetVariables(VariableScope.DeploymentStep, ds.DeploymentPlanStepID));
                    dpNode.Children.Add(dsNode);
                }
            }

            return nodes;
        }

        private IEnumerable<VariableDto> GetVariables(VariableScope scope, int scopeID)
        {
            var variables = _variableService.GetAllForScopeAs<VariableDto>(scope, scopeID);

            if (variables.IsNullOrEmpty())
                return Enumerable.Empty<VariableDto>();

            return variables.OrderBy(v => v.Name);
        }
    }
}
