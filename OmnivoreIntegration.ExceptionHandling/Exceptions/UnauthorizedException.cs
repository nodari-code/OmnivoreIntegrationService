using System;
using System.Net;
using System.Runtime.Serialization;

namespace OmnivoreIntegration.ExceptionHandling
{
    [Serializable]
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message) 
            : base(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(), message) { }

        protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
