using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Services
{
    public class UserService : CrudService<User>, IUserService
    {
        public UserService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public User GetByUsername(string username)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<User>()
                    .Get(u => u.Username == username)
                    .SingleOrDefault();
            }
        }

        public int GetCount()
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<User>()
                    .GetCount();
            }
        }
    }
}
