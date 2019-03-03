using System;
using System.Net;
using System.Runtime.Serialization;

namespace OmnivoreIntegration.ExceptionHandling
{
    [Serializable]
    public class BadGatewayException : ApiException
    {
        public BadGatewayException(string message) 
            : base(HttpStatusCode.BadGateway, HttpStatusCode.BadGateway.ToString(), message) { }
        protected BadGatewayException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
