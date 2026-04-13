namespace E_Commerce.Interface
{
    public interface IPaymentService
    {
        Task<string> ProcessPaymentAsync(int orderId, string paymentMethod);
    }
}
