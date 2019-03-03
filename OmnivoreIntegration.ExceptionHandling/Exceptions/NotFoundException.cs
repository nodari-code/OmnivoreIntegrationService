using System;
using System.Net;
using System.Runtime.Serialization;

namespace OmnivoreIntegration.ExceptionHandling
{
    [Serializable]
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message) 
            : base(HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), message) { }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
