using E_Commerce.DTOS;
using E_Commerce.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult> GetALL()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound("Product not found");
            return Ok(product);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult> Add(CreateProductDto dto)
        {
            var result = await _productService.AddProductAsync(dto);
            if (result != null)
                return Ok(result);
            return BadRequest("Failed to create product.");
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            var result = await _productService.UpdateProductAsync(id, dto);
            if (result != null)
                return Ok(result);
            return BadRequest("Failed to update product.");
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (result)
                return Ok("Product deleted successfully.");
            return NotFound("Product not found.");
        }
    }
}
