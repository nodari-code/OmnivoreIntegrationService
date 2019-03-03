using System;
using System.Net;
using System.Runtime.Serialization;

namespace OmnivoreIntegration.ExceptionHandling
{
    [Serializable]
    public class ServiceUnavailableException : ApiException
    {
        public ServiceUnavailableException(string message) 
            : base(HttpStatusCode.ServiceUnavailable, HttpStatusCode.ServiceUnavailable.ToString(), message) { }

        protected ServiceUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
