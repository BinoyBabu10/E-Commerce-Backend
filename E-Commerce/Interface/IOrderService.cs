using E_Commerce.DTOS;
using E_Commerce.Models;

namespace E_Commerce.Interface
{
    public interface IOrderService
    {
        Task<string> PlaceOrderAsync(int userId);

        Task<IEnumerable<OrderResponseDto>> GetOrdersByUserIdAsync(int userId);

        Task<OrderResponseDto> GetOrderByIdAsync(int orderId);
    }
}
