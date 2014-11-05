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
    public class DeploymentPlanParametersController
        : CrudApiController<DeploymentPlanParameterDto, DeploymentPlanParameter, int, IDeploymentPlanParameterService>
    {
        public DeploymentPlanParametersController(IDeploymentPlanParameterService service)
            : base(service)
        {
        }

        protected override int GetID(DeploymentPlanParameter entity)
        {
            return entity.DeploymentPlanParameterID;
        }

        protected override void SetID(DeploymentPlanParameter entity, int id)
        {
            entity.DeploymentPlanParameterID = id;
        }

        public IEnumerable<DeploymentPlanParameterDto> GetAllForPlan(short deploymentPlanID)
        {
            return _service.GetAllForPlanAs<DeploymentPlanParameterDto>(deploymentPlanID);
        }
    }
}
