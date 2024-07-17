using System.Security.Claims;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Helpers
{
    public static class ClaimStore
    {
        public static List<Claim> claims = new()
        {
            new Claim("Create Customer", "false"),
            new Claim("Edit Customer", "false"),
            new Claim("Delete Customer", "false"),
            new Claim("Create Book", "false"),
            new Claim("Update Book", "false"),
            new Claim("Delete Book", "false"),
            new Claim("Create Reviews", "false"),
            new Claim("Update Reviews", "false"),
            new Claim("Delete Reviews", "false"),
            new Claim("Get Users", "false")
        };
    }
}
