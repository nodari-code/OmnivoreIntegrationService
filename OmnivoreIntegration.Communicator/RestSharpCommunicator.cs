using System;
using System.Configuration;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

using RestSharp;
using RestSharp.Deserializers;

using OmnivoreIntegration.Dto;
using OmnivoreIntegration.Logging;
using OmnivoreIntegration.ExceptionHandling;

namespace OmnivoreIntegration.Communicator
{
    public class RestSharpCommunicator
    {        
        private readonly static string BASE_URI_OMNIVORE;
        private readonly static string BASE_URI_POS_INTEGRATION_SERVICE;
        private readonly static string OMNIVORE_API_KEY;
        private readonly static string POS_INTEGRATION_SERVICE_API_KEY;

        static RestSharpCommunicator()
        {
            OMNIVORE_API_KEY = (string)GetAppSetting(typeof(string), "OmnivoreApiKey");
            BASE_URI_OMNIVORE = (string)GetAppSetting(typeof(string), "OmnivoreUri");
            BASE_URI_POS_INTEGRATION_SERVICE = (string)GetAppSetting(typeof(string), "PosPaymentServiceUri");
            POS_INTEGRATION_SERVICE_API_KEY = (string)GetAppSetting(typeof(string), "PosPaymentServiceApiKey");
        }

        public INLogManager Logger { get; set; }

        /// <summary>
        /// Add payment under the ticket using payment from calling service as an Omnivore 3rd Party Payment. 
        /// </summary>
        /// <param name="ticketId">Ticket unique identifier</param>
        /// <param name="amountToPay">the amount in cents excluding tips to be paid</param>
        /// <param name="tipToPay">The amount of tips to be paid</param>
        /// <param name="tenderType">Identifier of party payment type. It changes depending on POS location.</param>
        /// <param name="typeName">Configurable name of tender type. 3rd_party for Omnivar virtual POS.</param>
        /// <returns>PaymentCompleted DTO</returns>
        public async Task<PaymentCompleted> AddPayment(string locationIdentifier, long transactionId, long ticketId, 
            int amountToPay, int tipToPay, string tenderType, string typeName)
        {
            Stopwatch watch;
            string errorMessage = string.Empty;

#if DEBUG
            watch = Stopwatch.StartNew();
            if(Logger !=null) Logger.LogTrace("RestClient creation.");
#endif

            string resource = string.Format("{0}/tickets/{1}/payments/", locationIdentifier, ticketId.ToString());

            RestClient restClient = new RestClient(BASE_URI_OMNIVORE);
            var restRequest = new RestRequest(resource, Method.POST);

            // Add HTTP Headers
            restRequest.AddHeader("Api-Key", OMNIVORE_API_KEY);
            restRequest.AddHeader("Content-Type", "application/json");
            
            //Add parameters to body as Json 
            restRequest.AddJsonBody(new
            {
                amount = amountToPay,
                tip = tipToPay,
                tender_type = tenderType,
                type = typeName
            });

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            if (Logger != null) Logger.LogTrace("Omnivore API call start.");

            IRestResponse asyncResp = await restClient.ExecuteTaskAsync(restRequest, cancellationTokenSource.Token);
            if (Logger != null) Logger.LogTrace("Omnivore API call end.");

            if(asyncResp == null) throw new InternalServerErrorException("IRestResponse is null.");

            PaymentCompleted paymentCompleted = null;
            RestResponse response = new RestResponse();
            response.Content = asyncResp.Content;
            JsonDeserializer deserializer = new JsonDeserializer();
            paymentCompleted = deserializer.Deserialize<PaymentCompleted>(response);
            paymentCompleted.Successful = asyncResp.IsSuccessful;
            
#if DEBUG
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            if (Logger != null) Logger.LogTrace(string.Format("AddPayment time: {0} msec.", elapsedMs.ToString()));
#endif
            if (Logger != null) Logger.LogDebug("Communicator: payment end");

            if (Logger != null) Logger.LogInfo(string.Format("Payment made. LocationId: {0} TransactionId: {1} TicketId: {2} Amount: {3} Tip: {4} Sucessful: {5}", 
                locationIdentifier, transactionId.ToString(), ticketId.ToString(), amountToPay.ToString(), tipToPay.ToString(), paymentCompleted.Successful.ToString()));
            return paymentCompleted;
        }

        public async Task<GSPRPaymentCallback> SetPaymentStateAsync(GSPRPaymentCallback paymentResult)
        {
            string resource = "api/Integration/SendPaymentCallback/";

            RestClient restClient = new RestClient(BASE_URI_POS_INTEGRATION_SERVICE);
            var restRequest = new RestRequest(resource, Method.POST);

            // Add HTTP Headers
            restRequest.AddHeader("POS_INTG_SQUIRREL_API_KEY", POS_INTEGRATION_SERVICE_API_KEY);
            restRequest.AddHeader("Content-Type", "application/json");

            //Add parameters to body as Json 
            restRequest.AddJsonBody(paymentResult);

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            if (Logger != null) Logger.LogTrace("POSPaymentService call start.");

            //Set payment state
            IRestResponse asyncResp = await restClient.ExecuteTaskAsync(restRequest, cancellationTokenSource.Token);
            if (Logger != null) Logger.LogTrace("POSPaymentService call end.");

            GSPRPaymentCallback statusSetResult = GetExecutionResult(paymentResult, asyncResp);

            if (Logger != null) Logger.LogDebug("Communicator: set payment state end");

            return statusSetResult;
        }

        #region Helper Methods

        protected static object GetAppSetting(Type expectedType, string key)
        {
            string value = ConfigurationManager.AppSettings.Get(key);
            try
            {
                if (expectedType == typeof(int)) return int.Parse(value);
                if (expectedType == typeof(string)) return value;

                throw new InternalServerErrorException(string.Format("Type {0} not supported.", expectedType.Name));
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException(string.Format("Config key:{0} was expected to be of type {1} but was not. {2}",
                    key, expectedType.ToString(), ex.Message));
            }
        }

        /// <summary>
        /// Handle errors returned by Omnivore.
        /// <ex>{ "errors": [{"description": "Invalid payment type", "error": "invalid_input", "fields": ["$.type"]}]}</ex>
        /// <ex>{ "errors": [{"description": "Cannot make ticket payment because the ticket is closed.", "error": "ticket_closed"}]}</ex>
        /// </summary>
        /// <param name="paymentCompleted">DTO with payment result data.</param>
        private void ThrowApiException(PaymentCompleted paymentCompleted)
        {
            if (paymentCompleted.Successful) return;
            LogErrorMessage(paymentCompleted);
            ErrorSlug errorSlaug = paymentCompleted.Errors.Where(x => !string.IsNullOrEmpty(x.Error)).FirstOrDefault();

            if(errorSlaug == null) throw new InternalServerErrorException("No error slug.");

            HttpStatusCode statusCode = errorSlaug.Error.ToHttpStatusCode();
            ApiException apiException = statusCode.ToApiException(errorSlaug.Description);
            throw apiException;                
        }

        private void LogErrorMessage(PaymentCompleted paymentCompleted)
        {
            string errorMessage = paymentCompleted.ToErrorMessage();
            if (!string.IsNullOrEmpty(errorMessage)) Logger.LogError(errorMessage);
        }

        private GSPRPaymentCallback GetExecutionResult(GSPRPaymentCallback paymentResult, IRestResponse asyncResp)
        {
            GSPRPaymentCallback statusSetResult = new GSPRPaymentCallback()
            {
                TransactionID = paymentResult.TransactionID
            };

            if (asyncResp == null)
            {
                statusSetResult.Success = false;
                statusSetResult.Error = "IRestResponse is null.";
                return statusSetResult;
            }

            statusSetResult.Success = asyncResp.IsSuccessful;

            if (!statusSetResult.Success)
            {
                statusSetResult.Error = asyncResp.ErrorMessage;
            }

            return statusSetResult;
        }

        #endregion Helper Methods
    }
}
