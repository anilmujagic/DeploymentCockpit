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
        private readonly ITargetGroupEnvironmentService _targetGroupEnvironmentService;

        public ProjectTargetService(IUnitOfWorkFactory unitOfWorkFactory,
            ITargetGroupEnvironmentService targetGroupEnvironmentService)
            : base(unitOfWorkFactory)
        {
            if (targetGroupEnvironmentService == null)
                throw new ArgumentNullException("targetGroupEnvironmentService");
            _targetGroupEnvironmentService = targetGroupEnvironmentService;
        }

        public IEnumerable<ProjectTarget> GetAllForTargetGroupAndEnvironment(short targetGroupID, short environmentID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<ProjectTarget>()
                    .Get<Target>(
                        t => t.TargetGroupID == targetGroupID && t.ProjectEnvironmentID == environmentID,
                        t => t.Target);
            }
        }

        public IEnumerable<TDto> GetAllForProjectAs<TDto>(short projectID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<ProjectTarget>()
                    .GetAs<TDto, TargetGroup, ProjectEnvironment, Target>(
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
                    .Get(t =>
                        t.TargetGroupID == targetGroupID
                        && t.ProjectEnvironmentID == projectEnvironmentID
                        && t.TargetID == targetID);

                return !projectTargets.IsNullOrEmpty();
            }
        }

        public override void Insert(ProjectTarget entity)
        {
            base.Insert(entity);
            _targetGroupEnvironmentService.CreateCombination(entity.TargetGroupID, entity.ProjectEnvironmentID);
        }

        public override void Update(ProjectTarget entity)
        {
            throw new InvalidOperationException("Don't update project target. Remove it and add a new one instead.");
        }

        public override void Delete(ProjectTarget entity)
        {
            var dbEntity = this.GetByKey(entity.ProjectTargetID);

            base.Delete(entity);
            
            using (var uow = _unitOfWorkFactory.Create())
            {
                var projectTargets = uow.Repository<ProjectTarget>().Get(t =>
                    t.TargetGroupID == dbEntity.TargetGroupID
                    && t.ProjectEnvironmentID == dbEntity.ProjectEnvironmentID);
                if (projectTargets.IsNullOrEmpty())
                    _targetGroupEnvironmentService.DeleteCombination(
                        dbEntity.TargetGroupID, dbEntity.ProjectEnvironmentID);
            }
        }
    }
}
