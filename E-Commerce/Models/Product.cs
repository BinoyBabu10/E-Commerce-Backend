using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
