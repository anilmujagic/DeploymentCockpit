using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Server.Controllers.Api
{
    public class TargetsController : CrudApiController<TargetDto, Target, short, ITargetService>
    {
        public TargetsController(ITargetService service)
            : base(service)
        {
        }

        public IEnumerable<TargetDto> Get()
        {
            return _service.GetAllWithCredentialAs<TargetDto>();
        }

        public HttpResponseMessage GetConfigFile(short id, bool generateConfigFile)
        {
            if (generateConfigFile != true)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Parameter 'generateConfigFile=true' must be set for config file to be returned.");

            var configFile = _service.GenerateConfigFile(id);
                
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(configFile);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "AppSettings.config";
            return response;
        }
    }
}