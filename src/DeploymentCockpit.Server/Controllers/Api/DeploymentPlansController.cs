using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Server.Controllers.Api
{
    public class DeploymentPlansController
        : CrudApiController<DeploymentPlanDto, DeploymentPlan, short, IDeploymentPlanService>
    {
        public DeploymentPlansController(IDeploymentPlanService service)
            : base(service)
        {
        }

        public IEnumerable<DeploymentPlanDto> GetAllForProject(short projectID)
        {
            return _service.GetAllForProjectAs<DeploymentPlanDto>(projectID);
        }
    }
}
