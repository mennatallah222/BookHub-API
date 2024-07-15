namespace API.Core.Features.Queries.Responses
{
    public class GetAllBooksResponses
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        //public bool IsDeleted { get; set; }
        //public int? OrderId { get; set; }

        public List<string> GenreNames { get; set; }
        public string? Image { get; set; }
        public List<string> Reviews { get; set; }


    }
}
