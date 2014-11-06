using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;
using Insula.Common;

namespace DeploymentCockpit.Server.Controllers.Api
{
    public class DeploymentJobsController
        : CrudApiController<DeploymentJobDto, DeploymentJob, int, IDeploymentJobService>
    {
        private readonly IVariableService _variableService;

        public DeploymentJobsController(IDeploymentJobService service, IVariableService variableService)
            : base(service, isDeleteEnabled: false)
        {
            if (variableService == null)
                throw new ArgumentNullException("variableService");
            _variableService = variableService;
        }

        protected override void OnAfterInsert(DeploymentJob entity, DeploymentJobDto dto)
        {
            if (!dto.Parameters.IsNullOrEmpty())
            {
                foreach (var p in dto.Parameters)
                {
                    _variableService.Insert(new Variable
                        {
                            Scope = VariableScope.DeploymentJob,
                            ScopeID = entity.DeploymentJobID,
                            Name = p.Name,
                            Value = p.Value
                        });
                }
            }
        }

        protected override void OnBeforeInsert(DeploymentJob entity, DeploymentJobDto dto)
        {
            entity.Status = DeploymentStatus.Queued;
            entity.SubmissionTime = DateTime.UtcNow;
            entity.StartTime = null;
            entity.EndTime = null;
        }

        protected override void OnBeforeUpdate(DeploymentJob entity, DeploymentJobDto dto)
        {
            var existingEntity = _service.GetByKey(entity.DeploymentJobID);
            entity.ProjectID = existingEntity.ProjectID;
            entity.SubmissionTime = existingEntity.SubmissionTime;
            entity.StartTime = existingEntity.StartTime;
            entity.EndTime = existingEntity.EndTime;
            entity.StatusKey = existingEntity.StatusKey;
            entity.ProductVersion = existingEntity.ProductVersion;
            entity.ProjectEnvironmentID = existingEntity.ProjectEnvironmentID;
            entity.DeploymentPlanID = existingEntity.DeploymentPlanID;
        }

        protected override DeploymentJob GetByKey(int id)
        {
            return _service.GetWithRelations(id);
        }

        public IEnumerable<DeploymentJobDto> GetAllForProject(short projectID)
        {
            return _service.GetAllForProjectAs<DeploymentJobDto>(projectID);
        }

        public HttpResponseMessage GetAllActive(bool allActive)
        {
            if (!allActive)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Parameter 'allActive=true' must be set to get all active deployment jobs.");

            return Request.CreateResponse(HttpStatusCode.OK, _service.GetAllActiveAs<DeploymentJobDto>());
        }
    }
}
