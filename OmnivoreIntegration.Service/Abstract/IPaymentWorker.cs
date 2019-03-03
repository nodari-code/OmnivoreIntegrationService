using System.Threading.Tasks;

namespace OmnivoreIntegration.Service
{
    public interface IPaymentWorker
    {
        Task Make3PartyPaymentAsync(string locationIdentifier,
            long transactionId, long ticketId, int amount, int tip, string tenderType, string type);
    }
}
