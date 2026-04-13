using E_Commerce.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("{userId}")]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult> PlaceOrder(int userId)
        {
            var result = await _orderService.PlaceOrderAsync(userId);
            if (result == "Cart not found") return NotFound(result);
            return Ok(result);
        }
        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult>GetOrders(int userId)
        {
            var orders=await _orderService.GetOrderByIdAsync(userId);
            return Ok(orders);
        }
        [HttpGet("details/{orderId}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult>GetOrderById(int orderId)
        {
            var order=await _orderService.GetOrderByIdAsync(orderId);
            if(order == null) return NotFound(orderId);

            return Ok(order);

        }

    }
}
