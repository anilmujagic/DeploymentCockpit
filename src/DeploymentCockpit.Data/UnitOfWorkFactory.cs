using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;

namespace DeploymentCockpit.Data
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            var ctx = new DeploymentCockpitEntities();
            ctx.Configuration.LazyLoadingEnabled = false;
            return new UnitOfWork(ctx);
        }
    }
}
