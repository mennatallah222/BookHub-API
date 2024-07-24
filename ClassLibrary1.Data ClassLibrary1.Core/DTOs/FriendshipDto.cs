namespace ClassLibrary1.Data_ClassLibrary1.Core.DTOs
{
    public class FriendshipDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int FriendId { get; set; }
        public string FriendName { get; set; }
        public string FriendshipStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
