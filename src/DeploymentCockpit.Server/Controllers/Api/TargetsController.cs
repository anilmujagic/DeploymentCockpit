using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Server.Controllers.Api
{
    public class TargetsController : CrudApiController<TargetDto, Target, short, ITargetService>
    {
        public TargetsController(ITargetService service)
            : base(service)
        {
        }

        protected override short GetID(Target entity)
        {
            return entity.TargetID;
        }

        protected override void SetID(Target entity, short id)
        {
            entity.TargetID = id;
        }

        public override TargetDto Get(short id)
        {
            return this.EntityToDto(this.ModifyEntityForResponse(_service.GetWithCredential(id)));
        }

        public IEnumerable<TargetDto> Get()
        {
            return _service.GetAllWithCredentialAs<TargetDto>();
        }
    }
}