using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Services
{
    public class DeploymentJobService : CrudService<DeploymentJob>, IDeploymentJobService
    {
        public DeploymentJobService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public DeploymentJob GetWithRelations(int id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<DeploymentJob>()
                    .GetAll(
                        j => j.DeploymentJobID == id,
                        j => j.DeploymentPlan,
                        j => j.ProjectEnvironment)
                    .SingleOrDefault();
            }
        }

        public IEnumerable<TDto> GetAllForProjectAs<TDto>(short projectID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<DeploymentJob>()
                    .GetAllAs<TDto, DeploymentPlan, ProjectEnvironment>(
                        j => j.ProjectID == projectID,
                        j => j.DeploymentPlan,
                        j => j.ProjectEnvironment);
            }
        }

        public IEnumerable<TDto> GetAllActiveAs<TDto>()
        {
            var activeStatusKeys = new string[]
                {
                    DeploymentStatus.Queued.ToString(),
                    DeploymentStatus.Running.ToString()
                };

            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<DeploymentJob>()
                    .GetAllAs<TDto, Project, DeploymentPlan, ProjectEnvironment>(
                        j => activeStatusKeys.Contains(j.StatusKey),
                        j => j.Project,
                        j => j.DeploymentPlan,
                        j => j.ProjectEnvironment);
            }
        }

        public DeploymentJob GetNextJobInTheQueue()
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<DeploymentJob>()
                    .GetAll(j => j.StatusKey == DeploymentStatus.Queued.ToString())
                    .OrderBy(j => j.SubmissionTime)
                    .FirstOrDefault();
            }
        }
    }
}
