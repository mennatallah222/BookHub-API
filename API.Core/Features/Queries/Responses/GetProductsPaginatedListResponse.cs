namespace API.Core.Features.Queries.Responses
{
    public class GetProductsPaginatedListResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }

        public string CategoryName { get; set; }
        public string? Image { get; set; }


        public GetProductsPaginatedListResponse(int productId, string name, decimal price, int quantity, string? description, string categoryName, string? image)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            Quantity = quantity;
            Description = description;
            CategoryName = categoryName;
            Image = image;

        }
    }
}
