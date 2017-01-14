using System.Collections.Generic;
using System.Linq;
using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Common;
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
                    .Get(
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
                    .GetAs<TDto, DeploymentPlan, ProjectEnvironment>(
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
                    .GetAs<TDto, Project, DeploymentPlan, ProjectEnvironment>(
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
                    .Get(j => j.StatusKey == DeploymentStatus.Queued.ToString())
                    .OrderBy(j => j.SubmissionTime)
                    .FirstOrDefault();
            }
        }

        public DeploymentJobDto ResolveDeploymentJobDto(string project, string plan, string version, string environment,
            IEnumerable<NameValuePair> parameters = null)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var projectID = uow.Repository<Project>()
                    .Get(p => p.ApiCode == project || p.Name == project)?
                    .FirstOrDefault()?.ProjectID;

                var planID = uow.Repository<DeploymentPlan>()
                    .Get(p => p.ApiCode == plan || p.Name == plan)?
                    .FirstOrDefault()?.DeploymentPlanID;

                var environmentID = uow.Repository<ProjectEnvironment>()
                    .Get(e => e.ApiCode == environment || e.Name == environment)?
                    .FirstOrDefault()?.ProjectEnvironmentID;

                return new DeploymentJobDto
                {
                    ProjectID = projectID,
                    DeploymentPlanID = planID,
                    ProductVersion = version,
                    ProjectEnvironmentID = environmentID,
                    Parameters = parameters
                };
            }
        }
    }
}
