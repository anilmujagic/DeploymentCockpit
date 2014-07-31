using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeploymentCockpit.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        IRepository<T> Repository<T>() where T : class;
    }
}
