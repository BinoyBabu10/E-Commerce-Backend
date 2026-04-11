using E_Commerce.DTOs;
using E_Commerce.DTOS;

namespace E_Commerce.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();

        Task<ProductResponseDto> GetProductByIdAsync(int id);

        Task<ProductResponseDto> AddProductAsync(CreateProductDto dto);

        Task<ProductResponseDto> UpdateProductAsync(int id, UpdateProductDto dto);

        Task<bool> DeleteProductAsync(int id);
    }
}