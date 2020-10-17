using Microsoft.EntityFrameworkCore;
using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using SocialMediaInfraestructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaInfraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SocialMediaContext db;

        public UserRepository(SocialMediaContext _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await db.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var users = await db.Users.FirstOrDefaultAsync(u => u.UserId == id);
            return users;
        }

    }
}
