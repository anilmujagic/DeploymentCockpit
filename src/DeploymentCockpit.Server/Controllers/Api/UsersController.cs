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
    public class UsersController : CrudApiController<UserDto, User, short, IUserService>
    {
        public UsersController(IUserService service)
            : base(service)
        {
        }

        protected override short GetID(User entity)
        {
            return entity.UserID;
        }

        protected override void SetID(User entity, short id)
        {
            entity.UserID = id;
        }

        public IEnumerable<UserDto> Get()
        {
            return this.GetAll();
        }

        public override HttpResponseMessage Delete(short id)
        {
            if (_service.GetCount() == 1)
                return Request.CreateResponse(HttpStatusCode.Conflict, new[] { "There must be at least one user." });

            return base.Delete(id);
        }
    }
}
