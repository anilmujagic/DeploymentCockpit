using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Services
{
    public class ScriptService : CrudService<Script>, IScriptService
    {
        public ScriptService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public Script GetWithParameters(short id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<Script>()
                    .GetAll(s => s.ScriptID == id, s => s.Parameters)
                    .SingleOrDefault();
            }
        }

        public IEnumerable<TDto> GetAllWithProjectAs<TDto>()
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<Script>()
                    .GetAllAs<TDto, Project>(s => s.Project);
            }
        }

        public IEnumerable<TDto> GetAllAvailableToProjectAs<TDto>(short projectID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<Script>()
                    .GetAllAs<TDto, Project>(
                        s => s.ProjectID == null || s.ProjectID == projectID,    
                        s => s.Project);
            }
        }
    }
}
