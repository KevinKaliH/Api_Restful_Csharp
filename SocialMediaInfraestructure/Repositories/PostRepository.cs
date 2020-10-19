using Microsoft.EntityFrameworkCore;
using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using SocialMediaInfraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaInfraestructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {

        public PostRepository(SocialMediaContext _db) : base(_db) {}

        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            return await entities.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
