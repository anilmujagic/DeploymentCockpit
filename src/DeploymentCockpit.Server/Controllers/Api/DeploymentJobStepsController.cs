using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Server.Controllers.Api
{
    public class DeploymentJobStepsController : ApiController
    {
        private readonly IDeploymentJobStepService _service;

        public DeploymentJobStepsController(IDeploymentJobStepService service)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            _service = service;
        }

        public IEnumerable<DeploymentJobStepFlatDto> Get(int deploymentJobID)
        {
            return _service.GetAllForDeploymentJobFlat(deploymentJobID);
        }

        public ExecutionDetails Get(Guid id)
        {
            return _service.GetExecutionDetails(id);
        }
    }
}