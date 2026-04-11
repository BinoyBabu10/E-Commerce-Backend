using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }

        public Order Order { get; set; }

        public string PaymentMethod { get; set; } // e.g., "Credit Card", "PayPal"

        public string Status { get; set; } // e.g., "Pending", "Completed", "Failed"

        public DateTime PaymentDate { get; set; }   
    }
}
