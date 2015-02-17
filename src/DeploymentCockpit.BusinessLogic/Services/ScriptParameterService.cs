using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Services
{
    public class ScriptParameterService : CrudService<ScriptParameter>, IScriptParameterService
    {
        public ScriptParameterService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IEnumerable<TDto> GetAllForScriptAs<TDto>(short scriptID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<ScriptParameter>()
                    .GetAs<TDto>(e => e.ScriptID == scriptID);
            }
        }
    }
}
