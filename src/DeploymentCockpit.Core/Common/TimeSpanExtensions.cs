using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.Common
{
    public static class TimeSpanExtensions
    {
        public static string ToDisplayString(this TimeSpan timeSpan)
        {
            return timeSpan.ToString(timeSpan.TotalDays >= 1 ? "d'.'hh':'mm':'ss" : "hh':'mm':'ss");
        }
    }
}
