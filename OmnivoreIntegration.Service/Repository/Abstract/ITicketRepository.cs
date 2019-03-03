using System.Threading.Tasks;

using OmnivoreIntegration.Dto;

namespace OmnivoreIntegration.Service
{
    public interface ITicketRepository
    {
        Task<GSPRPaymentCallback> AddPaymentAsync(string locationIdentifier, long transactionId, 
            long ticketId, int amount, int tip, string tenderType, string type);
    }
}