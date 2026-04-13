using E_Commerce.DTOS;
using E_Commerce.Interface;
using E_Commerce.Models;

namespace E_Commerce.Service
{
    public class PaymentService: IPaymentService
    {
        private readonly EDbContext _context;
        public PaymentService(EDbContext context)
        {
            _context = context;
        }
        public async Task<string> ProcessPaymentAsync(PaymentDto dto)
        {
            var order = await _context.Orders.FindAsync(dto.OrderId);
            if (order == null) return "Order not found";
           var payment =new Payment
            {
                OrderId = dto.OrderId,
                PaymentMethod = dto.PaymentMethod,
                PaymentDate = DateTime.UtcNow,
                Status = "Completed"
            };
            _context.Payments.Add(payment);
            order.Status = "Completed";
            await _context.SaveChangesAsync();
            return "Payment Processed Successfully";
        }
    }
}
