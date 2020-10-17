using Microsoft.EntityFrameworkCore;
using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using SocialMediaInfraestructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaInfraestructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext db;

        public PostRepository(SocialMediaContext _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<Post>> GetPosts() 
        {
            var posts = await db.Posts.ToListAsync();

            return posts;
        }

        public async Task<Post> GetPost(int id)
        {
            var posts = await db.Posts.FirstOrDefaultAsync(p => p.PostId == id);
            return posts;
        }

        public async Task InsertPost(Post _post)
        {
            db.Posts.Add(_post);

            await db.SaveChangesAsync();
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            var currentPost = await GetPost(post.PostId);
            currentPost.Date = post.Date;
            currentPost.Description = post.Description;
            currentPost.Image = post.Image;

            int rows = await db.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var currentPost = await GetPost(id);
            db.Posts.Remove(currentPost);

            int rows = await db.SaveChangesAsync();
            return rows > 0;
        }
    }
}
