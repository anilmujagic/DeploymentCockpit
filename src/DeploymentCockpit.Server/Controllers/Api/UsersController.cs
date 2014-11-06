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

        protected override void OnBeforeDelete(short id)
        {
            if (_service.GetCount() == 1)
                throw new ApiException(HttpStatusCode.Conflict, "There must be at least one user.");
        }
    }
}
