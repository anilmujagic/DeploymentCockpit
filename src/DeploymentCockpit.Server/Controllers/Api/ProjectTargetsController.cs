using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;
using DeploymentCockpit.Server.Common;

namespace DeploymentCockpit.Server.Controllers.Api
{
    public class ProjectTargetsController
        : CrudApiController<ProjectTargetDto, ProjectTarget, int, IProjectTargetService>
    {
        public ProjectTargetsController(IProjectTargetService service)
            : base(service, isPutEnabled: false)
        {
        }

        protected override void OnBeforeInsert(ProjectTarget entity, ProjectTargetDto dto)
        {
            var exists = _service.ProjectTargetExists(
                dto.TargetGroupID.Value,
                dto.ProjectEnvironmentID.Value,
                dto.TargetID.Value);
            if (exists)
                throw new ApiException(HttpStatusCode.Conflict, "Target is already added.");
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
