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
    public class ProjectTargetsController
        : CrudApiController<ProjectTargetDto, ProjectTarget, int, IProjectTargetService>
    {
        public ProjectTargetsController(IProjectTargetService service)
            : base(service)
        {
        }

        protected override int GetID(ProjectTarget entity)
        {
            return entity.ProjectTargetID;
        }

        protected override void SetID(ProjectTarget entity, int id)
        {
            entity.ProjectTargetID = id;
        }

        public override HttpResponseMessage Put(int id, ProjectTargetDto dto)
        {
            return Request.CreateResponse(HttpStatusCode.MethodNotAllowed);
        }

        public override HttpResponseMessage Post(ProjectTargetDto dto)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, this.GetValidationErrorMessages());

            var exists = _service.ProjectTargetExists(
                dto.TargetGroupID.Value,
                dto.ProjectEnvironmentID.Value,
                dto.TargetID.Value);
            if (exists)
                return Request.CreateResponse(HttpStatusCode.Conflict, new[] { "Target is already added." });

            return base.Post(dto);
        }

        public IEnumerable<object> GetAllForProject(short projectID)
        {
            return _service.GetAllForProjectAs<ProjectTargetDto>(projectID)
                .GroupBy(g => new { g.TargetGroupID, g.TargetGroupName })
                .Select(g => new
                    {
                        g.Key.TargetGroupID,
                        Name = g.Key.TargetGroupName,
                        Environments = g
                            .GroupBy(e => new { e.ProjectEnvironmentID, e.ProjectEnvironmentName })
                            .Select(e => new
                                {
                                    e.Key.ProjectEnvironmentID,
                                    Name = e.Key.ProjectEnvironmentName,
                                    Targets = e
                                        .Select(t => new
                                            {
                                                t.ProjectTargetID,
                                                t.TargetID,
                                                Name = t.TargetName
                                            })
                                        .ToList()
                                })
                            .ToList()
                    })
                .ToList();
        }
    }
}
