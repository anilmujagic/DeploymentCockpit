using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Interfaces
{
    public interface IUserService : ICrudService<User>
    {
        User GetByUsername(string username);
    }
}
