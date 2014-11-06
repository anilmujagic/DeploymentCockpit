using System;
using System.Net;
using System.Runtime.Serialization;

namespace DeploymentCockpit.Server.Common
{
    [Serializable]
    public class ApiException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; private set; }

        public ApiException(HttpStatusCode code)
        {
            this.HttpStatusCode = code;
        }

        public ApiException(HttpStatusCode code, string message)
            : base(message)
        {
            this.HttpStatusCode = code;
        }

        public ApiException(HttpStatusCode code, string message, Exception innerException)
            : base(message, innerException)
        {
            this.HttpStatusCode = code;
        }

        protected ApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.HttpStatusCode = (HttpStatusCode)info.GetValue("HttpStatusCode", typeof(HttpStatusCode));
        }
    }
}
