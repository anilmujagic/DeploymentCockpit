using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public IEnumerable<TDto> GetAllForPlanAs<TDto>(short deploymentPlanID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<DeploymentPlanParameter>()
                    .GetAllAs<TDto>(p => p.DeploymentPlanID == deploymentPlanID);
            }
        }
    }
}
