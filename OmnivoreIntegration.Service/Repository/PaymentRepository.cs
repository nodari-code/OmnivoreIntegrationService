using System.Threading.Tasks;

using OmnivoreIntegration.Dto;
using OmnivoreIntegration.Logging;

namespace OmnivoreIntegration.Service
{
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {
        public PaymentRepository(INLogManager nLogManager) : base(nLogManager) { }

        public async Task SetPaymentStateAsync(GSPRPaymentCallback paymentResult)
        {
            GSPRPaymentCallback statusSetResult = await Communicator.SetPaymentStateAsync(paymentResult);

            if (statusSetResult.Success)
            {
                if (Logger != null) Logger.LogInfo(string.Format("Payment state set. TransactionId: {0}", statusSetResult.TransactionID.ToString()));
            }
            else
            {
                if (Logger != null) Logger.LogInfo(string.Format("Cannot set payment state. TransactionId: {0}, Error: {1}",
                    statusSetResult.TransactionID.ToString(), statusSetResult.Error));
            }
        }
    }
}