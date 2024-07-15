namespace API.Core.Features.Queries.Responses
{
    public class GetBookssPaginatedListResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }

        public string GenresNames { get; set; }
        public string? Image { get; set; }


        public GetBookssPaginatedListResponse(int productId, string name, decimal price, int quantity, string? description, string genresNames, string? image)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            Quantity = quantity;
            Description = description;
            GenresNames = genresNames;
            Image = image;

        }
    }
}
