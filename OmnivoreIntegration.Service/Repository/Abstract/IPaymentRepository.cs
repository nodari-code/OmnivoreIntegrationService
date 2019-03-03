using System.Threading.Tasks;

using OmnivoreIntegration.Dto;

namespace OmnivoreIntegration.Service
{
    public interface IPaymentRepository
    {
        Task SetPaymentStateAsync(GSPRPaymentCallback paymentResult);
    }
}
