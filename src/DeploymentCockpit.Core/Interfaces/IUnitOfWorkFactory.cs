using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeploymentCockpit.Interfaces
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
