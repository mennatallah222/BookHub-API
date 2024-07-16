namespace ClassLibrary1.Data_ClassLibrary1.Core.Responses

{
    public class ManageUserRoleResponse
    {
        public int UserId { get; set; }
        public List<UserRoles> UserRoles { get; set; }
        public string? Message { get; set; }

    }
    public class UserRoles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }
    }
}
