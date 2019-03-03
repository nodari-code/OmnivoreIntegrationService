using System.Net.Http;
using System.Web.Http.Filters;

using OmnivoreIntegration.ExceptionHandling;

namespace OmnivoreIntegration.Service
{
    public sealed class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if(actionExecutedContext != null)
            {
                var exception = actionExecutedContext.Exception as ApiException;
                if (exception != null)
                {
                    actionExecutedContext.Response =
                        actionExecutedContext.Request.CreateResponse(exception.StatusCode, exception.Message.ToTransactionError());
                }
            }
        }
    }
}