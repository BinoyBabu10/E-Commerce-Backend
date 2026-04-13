using E_Commerce.DTOS;
using E_Commerce.Interface;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service
{
    public class ProductService : IProductService
    {
        private readonly EDbContext _context;

        public ProductService(EDbContext context)
        {
            _context = context;
        }

        // 📦 GET ALL PRODUCTS
        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                }).ToListAsync();

            if (products == null || !products.Any())
                throw new KeyNotFoundException("No products found");

            return products;
        }

        // 📦 GET PRODUCT BY ID
        public async Task<ProductResponseDto> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
        }

        // 📦 ADD PRODUCT
        public async Task<ProductResponseDto> AddProductAsync(CreateProductDto dto)
        {
            // 🔥 Basic validation
            if (dto.Price <= 0)
                throw new ArgumentException("Price must be greater than zero");

            if (dto.Stock < 0)
                throw new ArgumentException("Stock cannot be negative");

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageUrl = dto.ImageUrl
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
        }

        // 📦 UPDATE PRODUCT
        public async Task<ProductResponseDto> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            if (dto.Price <= 0)
                throw new ArgumentException("Price must be greater than zero");

            if (dto.Stock < 0)
                throw new ArgumentException("Stock cannot be negative");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.ImageUrl = dto.ImageUrl;

            await _context.SaveChangesAsync();

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
        }

        // 📦 DELETE PRODUCT
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}