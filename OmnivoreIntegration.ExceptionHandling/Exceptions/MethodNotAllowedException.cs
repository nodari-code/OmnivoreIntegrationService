using System;
using System.Net;
using System.Runtime.Serialization;

namespace OmnivoreIntegration.ExceptionHandling.Exceptions
{
    [Serializable]
    public class MethodNotAllowedException : ApiException
    {
        public MethodNotAllowedException(string message)
            : base(HttpStatusCode.MethodNotAllowed, HttpStatusCode.MethodNotAllowed.ToString(), message) { }
        protected MethodNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
