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
    public class DeploymentJobParametersController
        : CrudApiController<DeploymentJobParameterDto, DeploymentJobParameter, int, IDeploymentJobParameterService>
    {
        public DeploymentJobParametersController(IDeploymentJobParameterService service)
            : base(service)
        {
        }

        protected override int GetID(DeploymentJobParameter entity)
        {
            return entity.DeploymentJobParameterID;
        }

        protected override void SetID(DeploymentJobParameter entity, int id)
        {
            entity.DeploymentJobParameterID = id;
        }

        public IEnumerable<DeploymentJobParameterDto> GetAllForProject(short projectID)
        {
            return _service.GetAllForProject(projectID);
        }
    }
}
