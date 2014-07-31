using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface IDeploymentJobService : ICrudService<DeploymentJob>
    {
        DeploymentJob GetWithRelations(int id);
        IEnumerable<TDto> GetAllForProjectAs<TDto>(short projectID);

        DeploymentJob GetNextJobInTheQueue();
    }
}
