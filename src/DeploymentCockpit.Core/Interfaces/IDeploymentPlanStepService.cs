using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface IDeploymentPlanStepService : ICrudService<DeploymentPlanStep>
    {
        DeploymentPlanStep GetWithRelations(int id);
        IEnumerable<DeploymentPlanStep> GetAllForDeploymentPlan(short deploymentPlanID);
        IEnumerable<TDto> GetAllForDeploymentPlanAs<TDto>(short deploymentPlanID);
    }
}
