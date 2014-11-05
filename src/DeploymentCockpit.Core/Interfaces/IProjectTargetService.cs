using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface IProjectTargetService : ICrudService<ProjectTarget>
    {
        IEnumerable<ProjectTarget> GetAllForTargetGroupAndEnvironment(short targetGroupID, short environmentID);
        IEnumerable<TDto> GetAllForProjectAs<TDto>(short projectID);
        bool ProjectTargetExists(short targetGroupID, short projectEnvironmentID, short targetID);
    }
}
