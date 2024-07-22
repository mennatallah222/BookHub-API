namespace API.Core.Features.UserFeatures.Queries.Response
{
    public class GetUserByIDResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public List<string>? CurrentlyReading { get; set; }
        public List<string>? WantToReadList { get; set; }
        public List<string>? ReadList { get; set; }
    }
}
