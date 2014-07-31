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

        protected override short GetID(Script entity)
        {
            return entity.ScriptID;
        }

        protected override void SetID(Script entity, short id)
        {
            entity.ScriptID = id;
        }

        public override ScriptDto Get(short id)
        {
            return this.EntityToDto(this.ModifyEntityForResponse(_service.GetWithProject(id)));
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
