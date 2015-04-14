using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DeploymentCockpit.Server.Controllers.Api
{
    public class DashboardController : ApiController
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            if (dashboardService == null)
                throw new ArgumentNullException("dashboardService");
            _dashboardService = dashboardService;
        }

        public IEnumerable<ProjectVersionInfo> GetProjectVersionInfo()
        {
            return _dashboardService.GetProjectVersionInfo();
        }
    }
}
