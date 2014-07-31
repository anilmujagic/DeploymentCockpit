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
    public class ScriptParametersController
        : CrudApiController<ScriptParameterDto, ScriptParameter, int, IScriptParameterService>
    {
        public ScriptParametersController(IScriptParameterService service)
            : base(service)
        {
        }

        protected override int GetID(ScriptParameter entity)
        {
            return entity.ScriptParameterID;
        }

        protected override void SetID(ScriptParameter entity, int id)
        {
            entity.ScriptParameterID = id;
        }

        public IEnumerable<ScriptParameterDto> GetAllForScript(short scriptID)
        {
            return _service.GetAllForScriptAs<ScriptParameterDto>(scriptID);
        }
    }
}
