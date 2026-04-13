using E_Commerce.DTOS;
using E_Commerce.Interface;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Service
{
    public class UserService : IUserService
    {
        private readonly EDbContext _context;
        private readonly IConfiguration _config;
        public UserService(EDbContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }



        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return "User already exists";

            // 🔥 Normalize input (avoid case issues)
            var role = dto.Role?.Trim();

            // 🔥 Validate role
            if (role != "Admin" && role != "Customer")
                return "Invalid role. Only Admin or Customer allowed";

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User registered successfully";
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return "Invalid credentials";

            // 🔐 Create Claims
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
    };

            // 🔐 Create Key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 🔐 Create Token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(_config["Jwt:DurationInMinutes"])
                ),
                signingCredentials: creds
            );

            // 🔐 Return Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
