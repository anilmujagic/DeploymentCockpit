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
    public class DeploymentJobExecutionService : IDeploymentJobExecutionService
    {
        private readonly IDeploymentJobService _deploymentJobService;
        private readonly IDeploymentJobStepService _deploymentJobStepService;
        private readonly IDeploymentJobStepTargetService _deploymentJobStepTargetService;
        private readonly IDeploymentPlanStepService _deploymentPlanStepService;
        private readonly ITargetGroupService _targetGroupService;
        private readonly ITargetGroupEnvironmentService _targetGroupEnvironmentService;
        private readonly IProjectTargetService _projectTargetService;
        private readonly ITargetService _targetService;
        private readonly ICredentialService _credentialService;
        private readonly IScriptService _scriptService;
        private readonly IScriptExecutionService _scriptExecutionService;
        private readonly IVariableService _variableService;

        public DeploymentJobExecutionService(
            IDeploymentJobService deploymentJobService,
            IDeploymentJobStepService deploymentJobStepService,
            IDeploymentJobStepTargetService deploymentJobStepTargetService,
            IDeploymentPlanStepService deploymentPlanStepService,
            ITargetGroupService targetGroupService,
            ITargetGroupEnvironmentService targetGroupEnvironmentService,
            IProjectTargetService projectTargetService,
            ITargetService targetService,
            ICredentialService credentialService,
            IScriptService scriptService,
            IScriptExecutionService scriptExecutionService,
            IVariableService variableService)
        {
            if (deploymentJobService == null)
                throw new ArgumentNullException("deploymentJobService");
            _deploymentJobService = deploymentJobService;

            if (deploymentJobStepService == null)
                throw new ArgumentNullException("deploymentJobStepService");
            _deploymentJobStepService = deploymentJobStepService;

            if (deploymentJobStepTargetService == null)
                throw new ArgumentNullException("deploymentJobStepTargetService");
            _deploymentJobStepTargetService = deploymentJobStepTargetService;

            if (deploymentPlanStepService == null)
                throw new ArgumentNullException("deploymentPlanStepService");
            _deploymentPlanStepService = deploymentPlanStepService;

            if (targetGroupService == null)
                throw new ArgumentNullException("targetGroupService");
            _targetGroupService = targetGroupService;

            if (targetGroupEnvironmentService == null)
                throw new ArgumentNullException("targetGroupEnvironmentService");
            _targetGroupEnvironmentService = targetGroupEnvironmentService;

            if (projectTargetService == null)
                throw new ArgumentNullException("projectTargetService");
            _projectTargetService = projectTargetService;

            if (targetService == null)
                throw new ArgumentNullException("targetService");
            _targetService = targetService;

            if (credentialService == null)
                throw new ArgumentNullException("credentialService");
            _credentialService = credentialService;

            if (scriptService == null)
                throw new ArgumentNullException("scriptService");
            _scriptService = scriptService;

            if (scriptExecutionService == null)
                throw new ArgumentNullException("scriptExecutionService");
            _scriptExecutionService = scriptExecutionService;

            if (variableService == null)
                throw new ArgumentNullException("variableService");
            _variableService = variableService;
        }

        public void ExecuteNextDeploymentJob()
        {
            var job = _deploymentJobService.GetNextJobInTheQueue();
            if (job == null)
                return;

            Log.Info("{0}Processing Job {1}...", Environment.NewLine, job.DeploymentJobID);

            job.Status = DeploymentStatus.Running;
            job.StartTime = DateTime.UtcNow;
            _deploymentJobService.Update(job);

            try
            {
                this.ExecuteDeploymentSteps(job);
                job.Status = DeploymentStatus.Finished;
                Log.Success("Processing Job {0} DONE", job.DeploymentJobID);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                job.Status = DeploymentStatus.Failed;
                job.Errors = ex.GetAllMessages();
            }
            finally
            {
                job.EndTime = DateTime.UtcNow;
                _deploymentJobService.Update(job);
            }
        }

        private void ExecuteDeploymentSteps(DeploymentJob job)
        {
            var planSteps = _deploymentPlanStepService.GetEnabledForDeploymentPlan(job.DeploymentPlanID)
                .OrderBy(s => s.ExecutionOrder)
                .ToList();

            if (planSteps.IsNullOrEmpty())
                return;

            var targetGroupIDs = _targetGroupService.GetAllForProject(job.ProjectID)
                .OrderBy(g => g.Name)
                .Select(g => g.TargetGroupID)
                .ToArray();

            foreach (var planStep in planSteps)
            {
                var jobStep = new DeploymentJobStep
                    {
                        DeploymentJobID = job.DeploymentJobID,
                        DeploymentPlanStepID = planStep.DeploymentPlanStepID,
                        Status = DeploymentStatus.Running,
                        StartTime = DateTime.UtcNow,
                        ExecutionReference = Guid.NewGuid()
                    };
                _deploymentJobStepService.Insert(jobStep);

                try
                {
                    var script = _scriptService.GetWithParameters(planStep.ScriptID);

                    if (planStep.AllTargetGroups)
                    {
                        this.ExecuteDeploymentStepOnTargets(script, planStep, job, jobStep.DeploymentJobStepID,
                            targetGroupIDs);
                    }
                    else if (planStep.TargetGroupID.HasValue)
                    {
                        this.ExecuteDeploymentStepOnTargets(script, planStep, job, jobStep.DeploymentJobStepID,
                            planStep.TargetGroupID.Value);
                    }
                    else  // Execute on deployment server
                    {
                        jobStep.ExecutedScript = _variableService.ResolveVariables(script, planStep, job);
                        jobStep.ExecutionOutput = "Waiting for output...";
                        _deploymentJobStepService.Update(jobStep);

                        var descriptor = new ScriptJobDescriptor
                            {
                                ScriptType = script.ScriptType,
                                ScriptBody = jobStep.ExecutedScript,
                                SuccessKeywords = script.SuccessKeywords,
                                FailureKeywords = script.FailureKeywords
                            };
                        var result = _scriptExecutionService.ExecuteScript(descriptor);

                        jobStep.ExecutionOutput = result.Output;
                        if (!result.IsSuccessful)
                            throw new Exception("Script execution on deployment server was not successful.");
                    }

                    jobStep.Status = DeploymentStatus.Finished;
                }
                catch (Exception ex)
                {
                    jobStep.Status = DeploymentStatus.Failed;
                    throw new Exception("Deployment step [{0}] failed.".FormatString(planStep.Name), ex);
                }
                finally
                {
                    jobStep.EndTime = DateTime.UtcNow;
                    _deploymentJobStepService.Update(jobStep);
                }
            }
        }

        private void ExecuteDeploymentStepOnTargets(Script script, DeploymentPlanStep planStep, DeploymentJob job,
            int deploymentJobStepID, params short[] targetGroupIDs)
        {
            foreach (var targetGroupID in targetGroupIDs)
            {
                var targetGroupEnvironmentID = _targetGroupEnvironmentService
                    .GetCombinationID(targetGroupID, job.ProjectEnvironmentID);

                var projectTargets = _projectTargetService
                    .GetAllForTargetGroupAndEnvironment(targetGroupID, job.ProjectEnvironmentID)
                    .OrderBy(t => t.Target.Name)
                    .ToList();

                foreach (var projectTarget in projectTargets)
                {
                    var targetID = projectTarget.TargetID;

                    var jobStepTarget = new DeploymentJobStepTarget
                    {
                        DeploymentJobStepID = deploymentJobStepID,
                        TargetID = targetID,
                        Status = DeploymentStatus.Running,
                        StartTime = DateTime.UtcNow,
                        ExecutionReference = Guid.NewGuid()
                    };
                    _deploymentJobStepTargetService.Insert(jobStepTarget);

                    var target = _targetService.GetWithCredential(targetID);

                    try
                    {
                        var username = target.UsernameWithDomain;
                        var password = _credentialService.DecryptPassword(target.Credential.Password);
                        var tempPassword = Guid.NewGuid().ToString();

                        var scriptBody = _variableService.ResolveVariables(script, planStep, job,
                            targetGroupID, targetGroupEnvironmentID, projectTarget.ProjectTargetID,
                            target.ComputerName, username, tempPassword);

                        // Don't log real password.
                        jobStepTarget.ExecutedScript = scriptBody.Replace(tempPassword, "**********");
                        jobStepTarget.ExecutionOutput = "Waiting for output...";
                        _deploymentJobStepTargetService.Update(jobStepTarget);

                        scriptBody = scriptBody.Replace(tempPassword, password);

                        var descriptor = new ScriptJobDescriptor
                        {
                            ScriptType = script.ScriptType,
                            ScriptBody = scriptBody,
                            SuccessKeywords = script.SuccessKeywords,
                            FailureKeywords = script.FailureKeywords,
                            RemoteExecution = planStep.RemoteExecution,
                            TargetID = target.TargetID
                        };
                        var result = _scriptExecutionService.ExecuteScript(descriptor);

                        jobStepTarget.ExecutionOutput = result.Output;
                        if (!result.IsSuccessful)
                            throw new Exception("Script execution on target [{0}] was not successful."
                                .FormatString(target.Name));

                        jobStepTarget.Status = DeploymentStatus.Finished;
                    }
                    catch (Exception ex)
                    {
                        jobStepTarget.Status = DeploymentStatus.Failed;
                        throw new Exception("Deployment step [{0}] failed for target [{1}]."
                            .FormatString(planStep.Name, target.Name), ex);
                    }
                    finally
                    {
                        jobStepTarget.EndTime = DateTime.UtcNow;
                        _deploymentJobStepTargetService.Update(jobStepTarget);
                    }
                }
            }
        }
    }
}
