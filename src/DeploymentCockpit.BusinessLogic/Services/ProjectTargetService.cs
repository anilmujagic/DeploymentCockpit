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
    public class ProjectTargetService : CrudService<ProjectTarget>, IProjectTargetService
    {
        public ProjectTargetService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IEnumerable<ProjectTarget> GetAllForTargetGroupAndEnvironment(short targetGroupID, short environmentID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<ProjectTarget>()
                    .GetAll<Target>(
                        t => t.TargetGroupID == targetGroupID && t.ProjectEnvironmentID == environmentID,
                        t => t.Target);
            }
        }

        public IEnumerable<TDto> GetAllForProjectAs<TDto>(short projectID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<ProjectTarget>()
                    .GetAllAs<TDto, TargetGroup, ProjectEnvironment, Target>(
                        t => t.ProjectEnvironment.ProjectID == projectID && t.TargetGroup.ProjectID == projectID,
                        t => t.TargetGroup,
                        t => t.ProjectEnvironment,
                        t => t.Target);
            }
        }

        public bool ProjectTargetExists(short targetGroupID, short projectEnvironmentID, short targetID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var projectTargets = uow.Repository<ProjectTarget>()
                    .GetAll(t =>
                        t.TargetGroupID == targetGroupID
                        && t.ProjectEnvironmentID == projectEnvironmentID
                        && t.TargetID == targetID);

                return !projectTargets.IsNullOrEmpty();
            }
        }
    }
}
