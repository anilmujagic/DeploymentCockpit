using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;
using Insula.Common;

namespace DeploymentCockpit.Services
{
    public class DeploymentJobStepService : CrudService<DeploymentJobStep>, IDeploymentJobStepService
    {
        public DeploymentJobStepService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IEnumerable<DeploymentJobStepDto> GetAllForDeploymentJob(int deploymentJobID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var steps = uow.Repository<DeploymentJobStep>()
                    .GetAllAs<DeploymentJobStepDto, DeploymentPlanStep>(
                        s => s.DeploymentJobID == deploymentJobID,
                        s => s.DeploymentPlanStep);

                foreach (var step in steps)
                {
                    step.Targets = uow.Repository<DeploymentJobStepTarget>()
                        .GetAllAs<DeploymentJobStepTargetDto, Target>(
                            t => t.DeploymentJobStepID == step.DeploymentJobStepID,
                            t => t.Target);
                }

                return steps;
            }
        }

        public IEnumerable<DeploymentJobStepFlatDto> GetAllForDeploymentJobFlat(int deploymentJobID)
        {
            var flatList = new List<DeploymentJobStepFlatDto>();

            var steps = this.GetAllForDeploymentJob(deploymentJobID);
            if (!steps.IsNullOrEmpty())
            {
                foreach (var step in steps)
                {
                    flatList.Add(Mapper.Map<DeploymentJobStepFlatDto>(step));

                    if (!step.Targets.IsNullOrEmpty())
                    {
                        foreach (var target in step.Targets)
                        {
                            flatList.Add(Mapper.Map<DeploymentJobStepFlatDto>(target));
                        }
                    }
                }
            }

            return flatList;
        }

        public ExecutionDetails GetExecutionDetails(Guid executionReference)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var executionDetails = uow.Repository<DeploymentJobStepTarget>()
                    .GetAllAs<ExecutionDetails>(t => t.ExecutionReference == executionReference)
                    .SingleOrDefault();
                
                return executionDetails ??
                    uow.Repository<DeploymentJobStep>()
                    .GetAllAs<ExecutionDetails>(t => t.ExecutionReference == executionReference)
                    .SingleOrDefault();
            }
        }
    }
}
