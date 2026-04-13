using E_Commerce.DTOS;

namespace E_Commerce.Interface
{
    public interface IPaymentService
    {
        Task<string> ProcessPaymentAsync(PaymentDto dto);
    }
}
