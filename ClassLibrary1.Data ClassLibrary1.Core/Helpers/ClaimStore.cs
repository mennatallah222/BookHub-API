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
        };
    }
}
