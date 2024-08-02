namespace API.Core.Features.Commands.Responses
{
    public class CreateOrderResponse
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string ShippingAddress { get; set; } = "";
        public string PaymentStatus { get; set; } = "Pending";
        public string PaymentMethod { get; set; } = "";

        public ICollection<string> Products { get; set; }

    }
}
