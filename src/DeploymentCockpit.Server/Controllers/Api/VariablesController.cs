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
    public class VariablesController
        : CrudApiController<VariableDto, Variable, int, IVariableService>
    {
        public VariablesController(IVariableService service)
            : base(service)
        {
        }

        protected override int GetID(Variable entity)
        {
            return entity.VariableID;
        }

        protected override void SetID(Variable entity, int id)
        {
            entity.VariableID = id;
        }

        public IEnumerable<VariableDto> GetAllForScope(string scopeKey, int scopeID)
        {
            return _service.GetAllForScopeAs<VariableDto>(scopeKey, scopeID);
        }
    }
}
