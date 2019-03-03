using System;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;

using OmnivoreIntegration.Business;
using OmnivoreIntegration.ExceptionHandling;

namespace OmnivoreIntegration.Service
{
    [RoutePrefix("api")]
    public class TicketController : BaseApiController
    {
        private IPaymentWorker paymentWorker;

        public TicketController(IPaymentWorker paymentWorker) : base()
        {
            this.paymentWorker = paymentWorker;
        }

        [Route("payment")]
        public HttpResponseMessage Post([FromBody]PaymentRequest paymentRequest)
        {
            if (paymentRequest == null) throw new InternalServerErrorException("paymentRequest is null.");
            if (Logger != null) Logger.LogDebug("Start.");
            if (!ModelState.IsValid)
            {
                string errorMessage = ModelState.FirstErrorMessage();
                if (Logger != null) Logger.LogError(string.Format("{0} Transaction ID: {1}", errorMessage, paymentRequest.TransactionID));
                throw new BadRequestException(errorMessage.ToCombinedErrorMessage(paymentRequest.TransactionID));
            }

            ThirdPartyPayment payment = new ThirdPartyPayment(paymentRequest.BillAmount, paymentRequest.TipAmount)
            {
                TicketId = paymentRequest.BillNo,
                TransactionId = paymentRequest.TransactionID,
                LocationIdentifier = paymentRequest.LocationIdentifier,
                Amount = paymentRequest.BillAmount,
                Tip = paymentRequest.TipAmount,
                TenderTypeId = paymentRequest.PaymentTypeId,
                Type = paymentRequest.PaymentTypeName                
            };

            HostingEnvironment.QueueBackgroundWorkItem(async cancelationToken =>
            {
                if (Logger != null) Logger.LogDebug("Background worker start.");
                try
                {
                    await paymentWorker.Make3PartyPaymentAsync(payment.LocationIdentifier, payment.TransactionId,
                                             payment.TicketId, payment.Amount, payment.Tip, payment.TenderTypeId, payment.Type);
                }
                catch (Exception ex)
                {
                    if (Logger != null) Logger.LogError("Cannot add payment: " + ex.Message, ex);
                }
                if (Logger != null) Logger.LogDebug("Background worker end.");
            });

            if (Logger != null) Logger.LogDebug("End.");
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}