using E_Commerce.DTOS;
using E_Commerce.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult> ProcessPayment(PaymentDto dto)
        {
            var result = await _paymentService.ProcessPaymentAsync(dto);
            if (result == "Order not found") return NotFound(result);
            if (result == "Payment failed") return BadRequest(result);
            return Ok(result);
        }
    }
}
