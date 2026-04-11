using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } // e.g., "Pending", "Shipped", "Delivered"

        public ICollection<OrderItem> OrderItems { get; set; }

        public Payment Payment { get; set; }
    }
}
