using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DeploymentCockpit.Server.Controllers.Api
{
    public static class ApiControllerExtensions
    {
        public static IEnumerable<string> GetValidationErrorMessages(this ApiController controller)
        {
            return controller.ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
        }
    }
}