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
    public class DeploymentJobParameterService : CrudService<DeploymentJobParameter>, IDeploymentJobParameterService
    {
        public DeploymentJobParameterService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IEnumerable<DeploymentJobParameterDto> GetAllForProject(short projectID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<DeploymentJobParameter>()
                    .GetAllAs<DeploymentJobParameterDto>(p => p.ProjectID == projectID);
            }
        }
    }
}
