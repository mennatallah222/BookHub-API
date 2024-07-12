using API.Infrastructure.Infrastructures;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;

namespace API.Infrastructure.Interfaces
{
    public interface IRefreshTokenRepository : IGenericRepo<UserRefreshToken>
    {
    }
}
