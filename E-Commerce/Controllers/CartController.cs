using E_Commerce.DTOS;
using E_Commerce.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userID)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userID);
            if (cart == null) return NotFound("Cart not found");
            return Ok(cart);
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddToCart(int userId, AddToCartDto dto)
        {
            var result = await _cartService.AddToCartAsync(userId, dto);
            if (result == "Product not found") return NotFound(result);
            return Ok(result);

        }
        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int productId)
        {
            var result = await _cartService.RemoveFromCartAsync(userId, productId);
            if (result == "Cart not found") return NotFound(result);
            if (result == "Product not found in cart") return NotFound(result);
            return Ok(result);

        }
        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
                       var result = await _cartService.ClearCartAsync(userId);
            if (result == "Cart not found") return NotFound(result);
            return Ok(result);
        }

    }
}
