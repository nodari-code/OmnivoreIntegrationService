using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using Unity.Attributes;

using OmnivoreIntegration.Logging;

namespace OmnivoreIntegration.Service
{    
    public class BaseApiController : ApiController
    {
        [Dependency]
        protected INLogManager Logger { get; set; }

        protected void LogModelErrors(ModelStateDictionary modelState)
        {
            string errorMessage = string.Empty;

            if (modelState == null || modelState.Values.Count <= 0) return;
            foreach (ModelState state in modelState.Values)
            {
                foreach (ModelError error in state.Errors)
                {
                    errorMessage += error.ErrorMessage + Environment.NewLine;
                }
            }

            if(!string.IsNullOrEmpty(errorMessage))
            {
                Logger.LogWarn(errorMessage);
            }
        }
    }
}