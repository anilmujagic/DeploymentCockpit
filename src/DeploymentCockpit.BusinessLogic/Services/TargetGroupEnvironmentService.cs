using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Services
{
    public class TargetGroupEnvironmentService : CrudService<TargetGroupEnvironment>, ITargetGroupEnvironmentService
    {
        public TargetGroupEnvironmentService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IEnumerable<TargetGroupEnvironment> GetCombinationsForProject(short projectID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<TargetGroupEnvironment>()
                    .Get(e =>
                        e.TargetGroup.ProjectID == projectID
                        && e.ProjectEnvironment.ProjectID == projectID);
            }
        }

        public int? GetCombinationID(short targetGroupID, short projectEnvironmentID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var combination = uow.Repository<TargetGroupEnvironment>()
                    .Get(e =>
                        e.TargetGroupID == targetGroupID
                        && e.ProjectEnvironmentID == projectEnvironmentID)
                    .SingleOrDefault();

                if (combination == null)
                    return null;

                return combination.TargetGroupEnvironmentID;
            }
        }

        public void CreateCombination(short targetGroupID, short projectEnvironmentID)
        {
            var combinationID = this.GetCombinationID(targetGroupID, projectEnvironmentID);
            if (!combinationID.HasValue)
            {
                this.Insert(new TargetGroupEnvironment
                    {
                        TargetGroupID = targetGroupID,
                        ProjectEnvironmentID = projectEnvironmentID
                    });
            }
        }

        public void DeleteCombination(short targetGroupID, short projectEnvironmentID)
        {
            var combinationID = this.GetCombinationID(targetGroupID, projectEnvironmentID);
            if (combinationID.HasValue)
            {
                this.Delete(new TargetGroupEnvironment
                    {
                        TargetGroupEnvironmentID = combinationID.Value
                    });
            }
        }
    }
}
