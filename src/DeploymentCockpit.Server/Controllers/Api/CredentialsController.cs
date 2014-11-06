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
    public class CredentialsController : CrudApiController<CredentialDto, Credential, short, ICredentialService>
    {
        public CredentialsController(ICredentialService service)
            : base(service)
        {
        }

        protected override void OnBeforeInsert(Credential entity, CredentialDto dto)
        {
            entity.Password = _service.EncryptPassword(entity.Password);
        }

        protected override void OnBeforeUpdate(Credential entity, CredentialDto dto)
        {
            if (entity.Password.IsNullOrWhiteSpace())
            {
                var existingEntity = _service.GetByKey(entity.CredentialID);
                entity.Password = existingEntity.Password;
            }
            else
            {
                entity.Password = _service.EncryptPassword(entity.Password);
            }
        }

        protected override Credential ModifyEntityForResponse(Credential entity)
        {
            entity.Password = null;
            return entity;
        }

        public IEnumerable<CredentialDto> Get()
        {
            return this.GetAll();
        }
    }
}
