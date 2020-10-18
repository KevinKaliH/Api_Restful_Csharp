using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using SocialMediaInfraestructure.Data;
using System.Threading.Tasks;

namespace SocialMediaInfraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext db;
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Comment> commentRepository;

        public UnitOfWork(SocialMediaContext _db)
        {
            db = _db;
        }

        public IRepository<Post> PostRepository => postRepository ?? new BaseRepository<Post>(db);

        public IRepository<User> UserRepository => userRepository ?? new BaseRepository<User>(db);

        public IRepository<Comment> CommentRepository => commentRepository ?? new BaseRepository<Comment>(db);

        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
