using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insula.Common;

namespace DeploymentCockpit.Common
{
    public static class ExceptionExtensions
    {
        public static string GetAllMessages(this Exception exception)
        {
            if (exception.InnerException == null)
                return exception.Message;

            return exception.Message
                + Environment.NewLine + "[InnerException]: "
                + exception.InnerException.GetAllMessages();
        }

        public static string GetAllMessagesWithStackTraces(this Exception exception)
        {
            var messageWithStackTrace = exception.Message + Environment.NewLine + exception.StackTrace;

            if (exception.InnerException == null)
                return messageWithStackTrace;

            return messageWithStackTrace
                + Environment.NewLine + Environment.NewLine + "[InnerException]: "
                + exception.InnerException.GetAllMessagesWithStackTraces();
        }
    }
}
