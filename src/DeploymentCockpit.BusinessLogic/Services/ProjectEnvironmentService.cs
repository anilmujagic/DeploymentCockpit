using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Services
{
    public class ProjectEnvironmentService : CrudService<ProjectEnvironment>, IProjectEnvironmentService
    {
        public ProjectEnvironmentService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IEnumerable<TDto> GetAllForProjectAs<TDto>(short projectID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<ProjectEnvironment>()
                    .GetAs<TDto>(e => e.ProjectID == projectID);
            }
        }
    }
}
