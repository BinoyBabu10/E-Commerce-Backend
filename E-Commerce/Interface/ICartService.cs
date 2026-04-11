using E_Commerce.DTOS;
using E_Commerce.Models;

namespace E_Commerce.Interface
{
    public interface ICartService
    {
        Task<CartResponseDto> GetCartByUserIdAsync(int userId);

        Task<string> AddToCartAsync(int userId, AddToCartDto dto);

        Task<string> RemoveFromCartAsync(int userId, int productId);

        Task<string> ClearCartAsync(int userId);
    }
}
