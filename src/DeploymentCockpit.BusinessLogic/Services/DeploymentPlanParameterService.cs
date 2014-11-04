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
    public class DeploymentPlanParameterService : CrudService<DeploymentPlanParameter>, IDeploymentPlanParameterService
    {
        public DeploymentPlanParameterService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IEnumerable<DeploymentPlanParameterDto> GetAllForPlan(short deploymentPlanID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<DeploymentPlanParameter>()
                    .GetAllAs<DeploymentPlanParameterDto>(p => p.DeploymentPlanID == deploymentPlanID);
            }
        }
    }
}
