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
    public class DeploymentPlanStepsController
        : CrudApiController<DeploymentPlanStepDto, DeploymentPlanStep, int, IDeploymentPlanStepService>
    {
        public DeploymentPlanStepsController(IDeploymentPlanStepService service)
            : base(service)
        {
        }

        public IEnumerable<DeploymentPlanStepDto> GetAllForDeploymentPlan(short deploymentPlanID)
        {
            return _service.GetAllForDeploymentPlanAs<DeploymentPlanStepDto>(deploymentPlanID);
        }
    }
}
