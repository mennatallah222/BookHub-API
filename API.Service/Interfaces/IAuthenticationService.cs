using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using ClassLibrary1.Data_ClassLibrary1.Core.Helpers;

namespace API.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public JwtAuthResult GetJWTToken(User user);
    }
}
