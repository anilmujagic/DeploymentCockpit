using System.Collections.Generic;
using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Common;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface IDeploymentJobService : ICrudService<DeploymentJob>
    {
        DeploymentJob GetWithRelations(int id);
        IEnumerable<TDto> GetAllForProjectAs<TDto>(short projectID);
        IEnumerable<TDto> GetAllActiveAs<TDto>();
        DeploymentJob GetNextJobInTheQueue();
        DeploymentJobDto ResolveDeploymentJobDto(string project, string plan, string version, string environment,
            IEnumerable<NameValuePair> parameters = null);
        void CleanUpAbortedJobs();
    }
}
