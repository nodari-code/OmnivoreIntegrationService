using System;
using System.Net;
using System.Runtime.Serialization;

namespace OmnivoreIntegration.ExceptionHandling
{
    [Serializable]
    public class InternalServerErrorException : ApiException
    {
        public InternalServerErrorException(string message)
        : base(HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), message) { }
        protected InternalServerErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
