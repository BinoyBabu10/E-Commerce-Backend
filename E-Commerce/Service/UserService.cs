using E_Commerce.DTOS;
using E_Commerce.Interface;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service
{
    public class UserService : IUserService
    {
        private readonly EDbContext _context;
        public UserService(EDbContext context)
        {
            _context = context;
        }

        public async Task<string>RegisterAsync(RegisterDto dto)
        {
            if(_context.Users.Any(u => u.Email == dto.Email))
            {
                return "Email already exists.";
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "Customer"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "User registered successfully.";

        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
           var user=await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if(user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return "Invalid email or password.";
            }
            
            return "Login successful.";
        }

    }
}
