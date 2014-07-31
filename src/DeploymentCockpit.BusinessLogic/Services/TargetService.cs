using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Services
{
    public class TargetService : CrudService<Target>, ITargetService
    {
        public TargetService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public Target GetWithCredential(short id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<Target>()
                    .GetAll(t => t.TargetID == id, t => t.Credential)
                    .SingleOrDefault();
            }
        }

        public IEnumerable<TDto> GetAllWithCredentialAs<TDto>()
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<Target>()
                    .GetAllAs<TDto, Credential>(t => t.Credential);
            }
        }
    }
}
