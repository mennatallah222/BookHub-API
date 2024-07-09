using Microsoft.AspNetCore.Identity;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string FullNamw { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }

    }
}
