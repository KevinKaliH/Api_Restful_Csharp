using Microsoft.EntityFrameworkCore;
using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using SocialMediaInfraestructure.Data;
using System.Threading.Tasks;

namespace SocialMediaInfraestructure.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(SocialMediaContext _db) : base(_db) { }

        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
            return await entities.FirstOrDefaultAsync(x => x.User == login.User);
        }
    }
}
