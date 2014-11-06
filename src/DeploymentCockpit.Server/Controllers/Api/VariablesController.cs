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

        public IEnumerable<VariableDto> GetAllForScope(string scopeKey, int scopeID)
        {
            return _service.GetAllForScopeAs<VariableDto>(scopeKey, scopeID);
        }
    }
}
