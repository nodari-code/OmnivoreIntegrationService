using System;
using System.Net;
using System.Runtime.Serialization;

namespace OmnivoreIntegration.ExceptionHandling
{
    [Serializable]
    public class ForbiddenException : ApiException
    {
        public ForbiddenException(string message) 
            : base(HttpStatusCode.Forbidden, HttpStatusCode.Forbidden.ToString(), message) { }
        protected ForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
