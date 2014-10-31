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
    public class ProjectsController : CrudApiController<ProjectDto, Project, short, IProjectService>
    {
        public ProjectsController(IProjectService service)
            : base(service)
        {
        }

        protected override short GetID(Project entity)
        {
            return entity.ProjectID;
        }

        protected override void SetID(Project entity, short id)
        {
            entity.ProjectID = id;
        }

        public IEnumerable<ProjectDto> Get()
        {
            return this.GetAll();
        }

        public HttpResponseMessage Get(short projectID, bool variablesHierarchy)
        {
            if (!variablesHierarchy)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Parameter 'variablesHierarchy=true' must be set to get the hierarchy.");

            return Request.CreateResponse(HttpStatusCode.OK, _service.GetVariablesHierarchy(projectID));
        }
    }
}
