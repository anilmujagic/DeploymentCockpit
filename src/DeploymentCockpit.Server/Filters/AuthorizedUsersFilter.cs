using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Autofac.Integration.WebApi;
using DeploymentCockpit.Interfaces;
using Insula.Common;

namespace DeploymentCockpit.Server.Filters
{
    public class AuthorizedUsersFilter : IAutofacAuthorizationFilter
    {
        private readonly IUserService _userService;

        public AuthorizedUsersFilter(IUserService userService)
        {
            if (userService == null)
                throw new ArgumentNullException("userService");
            _userService = userService;
        }

        public void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var principal = actionContext.RequestContext.Principal;
            if (principal == null
                || principal.Identity == null
                || !principal.Identity.IsAuthenticated
                || principal.Identity.Name.IsNullOrWhiteSpace())
            {
                actionContext.Response =
                    actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized");
                return;
            }
            
            var user = _userService.GetByUsername(principal.Identity.Name);
            if (user == null)
            {
                actionContext.Response =
                    actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden");
                return;
            }
        }
    }
}