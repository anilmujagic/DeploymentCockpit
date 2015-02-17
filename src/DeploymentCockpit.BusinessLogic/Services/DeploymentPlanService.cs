using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Services
{
    public class DeploymentPlanService : CrudService<DeploymentPlan>, IDeploymentPlanService
    {
        public DeploymentPlanService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IEnumerable<TDto> GetAllForProjectAs<TDto>(short projectID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<DeploymentPlan>()
                    .GetAs<TDto>(p => p.ProjectID == projectID);
            }
        }
    }
}
