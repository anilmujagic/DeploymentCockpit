using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;
using Insula.Common;

namespace DeploymentCockpit.Server.Controllers.Api
{
    public class ScriptJobsController : ApiController
    {
        private readonly IScriptExecutionService _scriptExecutionService;

        public ScriptJobsController(IScriptExecutionService scriptExecutionService)
        {
            if (scriptExecutionService == null)
                throw new ArgumentNullException("scriptExecutionService");
            _scriptExecutionService = scriptExecutionService;
        }

        [HttpPost]
        public HttpResponseMessage Post(ScriptJobDescriptor descriptor)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, this.GetValidationErrorMessages());

            var result = _scriptExecutionService.ExecuteScript(descriptor);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
