using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.Common
{
    public static class DateTimeExtensions
    {
        public static string GetDisplayValue(this DateTime dateTime)
        {
            return dateTime.ToString(DomainContext.DateTimeFormatString);
        }

        public static string GetDisplayValue(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return "-";

            return dateTime.Value.ToString(DomainContext.DateTimeFormatString);
        }
    }
}
