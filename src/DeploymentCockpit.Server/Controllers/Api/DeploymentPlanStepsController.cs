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

        protected override int GetID(DeploymentPlanStep entity)
        {
            return entity.DeploymentPlanStepID;
        }

        protected override void SetID(DeploymentPlanStep entity, int id)
        {
            entity.DeploymentPlanStepID = id;
        }

        protected override DeploymentPlanStep OnGetByKey(int id)
        {
            return _service.GetWithRelations(id);
        }

        public IEnumerable<DeploymentPlanStepDto> GetAllForDeploymentPlan(short deploymentPlanID)
        {
            return _service.GetAllForDeploymentPlanAs<DeploymentPlanStepDto>(deploymentPlanID);
        }
    }
}
