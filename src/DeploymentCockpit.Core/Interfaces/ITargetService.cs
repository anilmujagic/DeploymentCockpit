using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface ITargetService : ICrudService<Target>
    {
        Target GetWithCredential(short id);
        IEnumerable<TDto> GetAllWithCredentialAs<TDto>();
        string GenerateConfigFile(short id);
    }
}
