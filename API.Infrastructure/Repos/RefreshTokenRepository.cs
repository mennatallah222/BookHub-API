using API.Infrastructure.Data;
using API.Infrastructure.Infrastructures;
using API.Infrastructure.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repos
{
    public class RefreshTokenRepository : GenericRepo<UserRefreshToken>, IRefreshTokenRepository
    {
        private readonly DbSet<UserRefreshToken> _tokens;
        public RefreshTokenRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _tokens = dBContext.Set<UserRefreshToken>();
        }
    }
}
