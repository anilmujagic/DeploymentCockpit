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
    public class ProjectEnvironmentsController
        : CrudApiController<ProjectEnvironmentDto, ProjectEnvironment, short, IProjectEnvironmentService>
    {
        public ProjectEnvironmentsController(IProjectEnvironmentService service)
            : base(service)
        {
        }

        public IEnumerable<ProjectEnvironmentDto> GetAllForProject(short projectID)
        {
            return _service.GetAllForProjectAs<ProjectEnvironmentDto>(projectID);
        }
    }
}
