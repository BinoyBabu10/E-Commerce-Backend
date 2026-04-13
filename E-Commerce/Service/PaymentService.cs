using E_Commerce.DTOS;
using E_Commerce.Interface;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly EDbContext _context;

        public PaymentService(EDbContext context)
        {
            _context = context;
        }

        public async Task<string> ProcessPaymentAsync(PaymentDto dto)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == dto.OrderId);

            // ❌ Order not found → throw exception
            if (order == null)
                throw new KeyNotFoundException("Order not found");

            // ❌ Prevent duplicate payment
            var existingPayment = await _context.Payments
                .FirstOrDefaultAsync(p => p.OrderId == dto.OrderId);

            if (existingPayment != null)
                throw new ArgumentException("Payment already completed for this order");

            // ❌ Optional: check if already completed
            if (order.Status == "Completed")
                throw new ArgumentException("Order is already completed");

            var payment = new Payment
            {
                OrderId = dto.OrderId,
                PaymentMethod = dto.PaymentMethod,
                PaymentDate = DateTime.UtcNow,
                Status = "Completed"
            };

            _context.Payments.Add(payment);

            // ✅ Update order status
            order.Status = "Completed";

            await _context.SaveChangesAsync();

            return "Payment processed successfully";
        }
    }
}