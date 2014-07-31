using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface IDeploymentJobStepService : ICrudService<DeploymentJobStep>
    {
        IEnumerable<DeploymentJobStepDto> GetAllForDeploymentJob(int deploymentJobID);
        IEnumerable<DeploymentJobStepFlatDto> GetAllForDeploymentJobFlat(int deploymentJobID);
        ExecutionDetails GetExecutionDetails(Guid executionReference);
    }
}
