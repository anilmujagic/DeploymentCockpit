using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Services
{
    public class TargetGroupService : CrudService<TargetGroup>, ITargetGroupService
    {
        public TargetGroupService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IEnumerable<TargetGroup> GetAllForProject(short projectID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<TargetGroup>()
                    .GetAll(g => g.ProjectID == projectID);
            }
        }

        public IEnumerable<TDto> GetAllForProjectAs<TDto>(short projectID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<TargetGroup>()
                    .GetAllAs<TDto>(g => g.ProjectID == projectID);
            }
        }
    }
}
