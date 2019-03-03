using System;
using System.Threading.Tasks;

using Unity.Attributes;

using OmnivoreIntegration.Dto;
using OmnivoreIntegration.Logging;
using OmnivoreIntegration.ExceptionHandling;

namespace OmnivoreIntegration.Service
{
    public class PaymentWorker : IPaymentWorker
    {
        private ITicketRepository ticketRepository;
        private IPaymentRepository paymentRepository;

        public PaymentWorker(ITicketRepository ticketRepository, IPaymentRepository paymentRepository)
        {
            this.ticketRepository = ticketRepository;
            this.paymentRepository = paymentRepository;
        }

        [Dependency]
        protected INLogManager Logger { get; set; }

        public async Task Make3PartyPaymentAsync(string locationIdentifier,
            long transactionId, long ticketId, int amount, int tip, string tenderType, string type)
        {
            GSPRPaymentCallback callbackDto = new GSPRPaymentCallback();
            try
            {
                callbackDto = await ticketRepository.AddPaymentAsync(
                    locationIdentifier, transactionId, ticketId, amount, tip, tenderType, type);
                if(Logger!= null) Logger.LogInfo("Payment added.");
            }
            catch(ApiException ex)
            {
                callbackDto = new GSPRPaymentCallback()
                {
                    Error = ex.Message,
                    Success = false,
                    TransactionID = transactionId
                };
                if (Logger != null) Logger.LogError("An error has occurred while adding payment.", ex);
            }

            //Record execution result into UserPaymentPOSTransaction table
            //TODO: add call to the endpoint(TBD) and pass to it GSPRPaymentCallback
            try
            {
                await paymentRepository.SetPaymentStateAsync(callbackDto);
                if (Logger != null) Logger.LogInfo("Payment state set.");
            }
            catch(Exception ex)
            {
                if (Logger != null) Logger.LogError("An error has occurred while setting payment state.", ex);
            }
        }
    }
}