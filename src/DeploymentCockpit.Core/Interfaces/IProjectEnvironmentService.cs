using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface IProjectEnvironmentService : ICrudService<ProjectEnvironment>
    {
        IEnumerable<TDto> GetAllForProjectAs<TDto>(short projectID);
    }
}
