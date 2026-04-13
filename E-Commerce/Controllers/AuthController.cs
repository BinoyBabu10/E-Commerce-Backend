using E_Commerce.DTOS;
using E_Commerce.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Regiter(RegisterDto dto)
        {
            var result = await _userService.RegisterAsync(dto);
            if (result == "User registered successfully.")
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _userService.LoginAsync(dto);
            if (result == "Invalid email or password.")
                return Unauthorized(result);
            return Ok(result);
        }
    }
}
