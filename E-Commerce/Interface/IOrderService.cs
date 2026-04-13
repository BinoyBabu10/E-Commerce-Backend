using E_Commerce.Models;

namespace E_Commerce.Interface
{
    public interface IOrderService
    {
        Task<string> PlaceOrderAsync(int userId);
        Task<IEnumerable<Order>>GetOrderByUserIdAsync(int userId);

        Task<Order>GetOrderByIdAsync(int orderId);
    }
}
