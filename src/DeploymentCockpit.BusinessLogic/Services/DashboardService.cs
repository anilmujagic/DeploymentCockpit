using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.Services
{
    public class DashboardService : DataService, IDashboardService
    {
        public DashboardService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IEnumerable<ProjectVersionInfo> GetProjectVersionInfo()
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.DashboardRepository.GetProjectVersionInfo();
            }
        }
    }
}
