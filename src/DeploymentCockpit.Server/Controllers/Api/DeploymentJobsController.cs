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
    public class DeploymentJobsController
        : CrudApiController<DeploymentJobDto, DeploymentJob, int, IDeploymentJobService>
    {
        public DeploymentJobsController(IDeploymentJobService service)
            : base(service)
        {
        }

        protected override int GetID(DeploymentJob entity)
        {
            return entity.DeploymentJobID;
        }

        protected override void SetID(DeploymentJob entity, int id)
        {
            entity.DeploymentJobID = id;
        }

        protected override DeploymentJob ModifyEntityForInsert(DeploymentJob entity)
        {
            entity.Status = DeploymentStatus.Queued;
            entity.SubmissionTime = DateTime.UtcNow;
            entity.StartTime = null;
            entity.EndTime = null;

            return entity;
        }

        protected override DeploymentJob ModifyEntityForUpdate(DeploymentJob entity)
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

            return entity;
        }

        public override HttpResponseMessage Delete(int id)
        {
            return Request.CreateResponse(HttpStatusCode.MethodNotAllowed);
        }

        public override DeploymentJobDto Get(int id)
        {
            return this.EntityToDto(this.ModifyEntityForResponse(_service.GetWithRelations(id)));
        }

        public IEnumerable<DeploymentJobDto> GetAllForProject(short projectID)
        {
            return _service.GetAllForProjectAs<DeploymentJobDto>(projectID);
        }

        public HttpResponseMessage GetAllActive(bool allActive)
        {
            if (allActive != true)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Parameter 'allActive=true' must be set to get all active deployment jobs.");

            return Request.CreateResponse(HttpStatusCode.OK, _service.GetAllActiveAs<DeploymentJobDto>());
        }
    }
}
