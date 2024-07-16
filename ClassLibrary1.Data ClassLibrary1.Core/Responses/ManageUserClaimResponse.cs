namespace ClassLibrary1.Data_ClassLibrary1.Core.Responses
{
    public class ManageUserClaimResponse
    {
        public int UserId { get; set; }
        public List<UserClaims> UserClaims { get; set; }
        public string? Message { get; set; }

    }
    public class UserClaims
    {
        public string Type { get; set; }
        public bool Value { get; set; }
    }
}
