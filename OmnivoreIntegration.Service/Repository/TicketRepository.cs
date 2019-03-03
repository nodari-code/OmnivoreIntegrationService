using System.Threading.Tasks;

using OmnivoreIntegration.Dto;
using OmnivoreIntegration.Logging;

namespace OmnivoreIntegration.Service
{
    public class TicketRepository : BaseRepository, ITicketRepository
    {
        public TicketRepository(INLogManager nLogManager) : base(nLogManager) { }

        public async Task<GSPRPaymentCallback> AddPaymentAsync(string locationIdentifier, long transactionId, 
            long ticketId, int amount, int tip, string tenderType, string type)
        {
            string generalErrorMessage = "Cannot add payment.";

            PaymentCompleted response = await Communicator.AddPayment(locationIdentifier, transactionId, ticketId, amount, tip, tenderType, type);

            GSPRPaymentCallback calbackDto = new GSPRPaymentCallback()
            {
                TransactionID = transactionId,
                Success = (response == null) ? false : response.Successful,
            };

            if (!calbackDto.Success)
            {
                Logger.LogWarn(generalErrorMessage);
                string combinedErrorMessage = response.ToErrorMessage();
                calbackDto.Error = 
                    (string.IsNullOrEmpty(combinedErrorMessage)) ? generalErrorMessage : combinedErrorMessage;
            }
            
            return calbackDto;
        }
    }
}