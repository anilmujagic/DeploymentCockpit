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
    public class TargetGroupsController
        : CrudApiController<TargetGroupDto, TargetGroup, short, ITargetGroupService>
    {
        public TargetGroupsController(ITargetGroupService service)
            : base(service)
        {
        }

        public IEnumerable<TargetGroupDto> GetAllForProject(short projectID)
        {
            return _service.GetAllForProjectAs<TargetGroupDto>(projectID);
        }
    }
}
