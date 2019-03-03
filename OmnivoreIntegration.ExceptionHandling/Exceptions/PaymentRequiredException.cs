using System;
using System.Net;
using System.Runtime.Serialization;

namespace OmnivoreIntegration.ExceptionHandling.Exceptions
{
    [Serializable]
    public class PaymentRequiredException : ApiException
    {
        public PaymentRequiredException(string message)
            : base(HttpStatusCode.PaymentRequired, HttpStatusCode.PaymentRequired.ToString(), message) { }

        protected PaymentRequiredException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
