using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface ITargetGroupEnvironmentService : ICrudService<TargetGroupEnvironment>
    {
        IEnumerable<TargetGroupEnvironment> GetCombinationsForProject(short projectID);
        int? GetCombinationID(short targetGroupID, short projectEnvironmentID);
        void CreateCombination(short targetGroupID, short projectEnvironmentID);
        void DeleteCombination(short targetGroupID, short projectEnvironmentID);
    }
}
