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
    public class ScriptsController : CrudApiController<ScriptDto, Script, short, IScriptService>
    {
        public ScriptsController(IScriptService service)
            : base(service)
        {
        }

        public IEnumerable<ScriptDtoForList> Get(short? projectID = null)
        {
            if (projectID.HasValue)
                return _service.GetAllAvailableToProjectAs<ScriptDtoForList>(projectID.Value);
            else
                return _service.GetAllWithProjectAs<ScriptDtoForList>();
        }
    }
}
