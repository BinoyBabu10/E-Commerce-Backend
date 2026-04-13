using E_Commerce.DTOS;
using E_Commerce.Interface;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service
{
    public class CartService : ICartService
    {
        private readonly EDbContext _context;

        public CartService(EDbContext context)
        {
            _context = context;
        }

        // 🛒 GET CART
        public async Task<CartResponseDto> GetCartByUserIdAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                throw new KeyNotFoundException("Cart not found");

            var cartItems = cart.CartItems.Select(ci => new CartItemDto
            {
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                Quantity = ci.Quantity,
                Price = ci.Product.Price
            }).ToList();

            var total = cartItems.Sum(ci => ci.Price * ci.Quantity);

            return new CartResponseDto
            {
                CartId = cart.Id,
                Items = cartItems,
                TotalPrice = total
            };
        }

        // 🛒 ADD TO CART
        public async Task<string> AddToCartAsync(int userId, AddToCartDto dto)
        {
            var product = await _context.Products.FindAsync(dto.ProductId);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            var cart = await _context.Carts
                .Include(c => c.CartItems) // 🔥 FIXED BUG (important)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                });
            }

            await _context.SaveChangesAsync();

            return "Added to cart successfully";
        }

        // 🛒 REMOVE ITEM
        public async Task<string> RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                throw new KeyNotFoundException("Cart not found");

            var item = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (item == null)
                throw new KeyNotFoundException("Item not found in cart");

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();

            return "Item removed successfully";
        }

        // 🛒 CLEAR CART
        public async Task<string> ClearCartAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                throw new KeyNotFoundException("Cart not found");

            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();

            return "Cart cleared successfully";
        }
    }
}