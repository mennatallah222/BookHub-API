namespace ClassLibrary1.Data_ClassLibrary1.Core.DTOs
{
    public class OrderDTOs
    {
        public int OrderId { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CustomersOrdersDTO> Products { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
