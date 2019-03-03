using System;
using System.Net;
using System.Runtime.Serialization;

namespace OmnivoreIntegration.ExceptionHandling
{
    [Serializable]
    public class BadRequestException : ApiException
    {
        public BadRequestException(string message) 
            : base(HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), message) { }
        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
