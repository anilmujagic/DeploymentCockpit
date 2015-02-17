using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;

namespace DeploymentCockpit.Services
{
    public abstract class DataService
    {
        protected DataService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException("unitOfWorkFactory");

            _unitOfWorkFactory = unitOfWorkFactory;
        }

        protected readonly IUnitOfWorkFactory _unitOfWorkFactory;
    }
}
