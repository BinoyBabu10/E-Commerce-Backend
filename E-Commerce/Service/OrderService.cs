using E_Commerce.DTOS;
using E_Commerce.Interface;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service
{
    public class OrderService : IOrderService
    {
        private readonly EDbContext _context;
        public OrderService(EDbContext context)
        {
            _context = context;
        }

        //place Order
        public async Task<string> PlaceOrderAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
                return "Cart is Empty";
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                OrderItems = new List<OrderItem>()
            };
            decimal total = 0;
            foreach (var item in cart.CartItems)
            {
                if (item.Product.Stock < item.Quantity)
                    return $"Insufficient stock for {item.Product.Name}";
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };
                total += item.Quantity * item.Product.Price;
                order.OrderItems.Add(orderItem);
            }
            order.TotalAmount = total;
            _context.Orders.Add(order);

            //clear cart
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
            return "Order Placed Successfully";
        }

        //get order by user id
        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByUserIdAsync(int userId)
        {
            var order = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return order.Select(o => new OrderResponseDto
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                Items = o.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            });
        }

        public async Task<OrderResponseDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null) return null;
            return new OrderResponseDto
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                Items = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };
        }
    }
}
