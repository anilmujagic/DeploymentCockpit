using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface ITargetGroupService : ICrudService<TargetGroup>
    {
        IEnumerable<TargetGroup> GetAllForProject(short projectID);
        IEnumerable<TDto> GetAllForProjectAs<TDto>(short projectID);
    }
}
