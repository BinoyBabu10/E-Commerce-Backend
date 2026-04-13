using E_Commerce.DTOS;
using E_Commerce.Interface;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service
{
    public class CartService:ICartService
    {
        private readonly EDbContext _context;
        public CartService(EDbContext context)
        {
            _context = context;
        }

        public async Task<CartResponseDto> GetCartByUserIdAsync(int userId)
        {
            var cart=await _context.Carts
                .Include(c=>c.CartItems)
                .ThenInclude(ci=>ci.Product)
                .FirstOrDefaultAsync(c=>c.UserId==userId);
            if(cart==null) return null;
            var cartItems=cart.CartItems.Select(ci=>new CartItemDto
            {
                ProductId=ci.ProductId,
                ProductName=ci.Product.Name,
                Quantity=ci.Quantity,
                Price=ci.Product.Price
            }).ToList();
            var total =cartItems.Sum(ci=>ci.Price*ci.Quantity);
            return new CartResponseDto
            {
                CartId = cart.Id,
                Items = cartItems,
                TotalPrice = total
            };
        }

        public async Task<string>AddToCartAsync(int userId,AddToCartDto dto)
        {
            var product=await _context.Products.FindAsync(dto.ProductId);
            if(product==null) return "Product not found";
            var cart=await _context.Carts.FirstOrDefaultAsync(c=>c.UserId==userId);
            if(cart==null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }
            var existingItem=cart.CartItems.FirstOrDefault(ci=>ci.ProductId==dto.ProductId);
            if(existingItem!=null)
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
            return "Added to Cart";
        }

        public async Task<string> RemoveFromCartAsync(int userId,int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
                return "Cart not Found";

            var item = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (item == null)
                return "Item not found in Cart";
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return "Item removed";

        }

        public async Task<string> ClearCartAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
                return "Cart not found";

            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
            return "Cart Cleared";
        }
    }
}
