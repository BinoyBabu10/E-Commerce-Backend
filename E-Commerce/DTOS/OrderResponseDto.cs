namespace E_Commerce.DTOS
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

        public string Status { get; set; }
    }
}
