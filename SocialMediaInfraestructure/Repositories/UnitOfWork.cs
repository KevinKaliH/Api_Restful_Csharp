using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using SocialMediaInfraestructure.Data;
using System.Threading.Tasks;

namespace SocialMediaInfraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext db;
        private readonly IPostRepository postRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Comment> commentRepository;
        private readonly ISecurityRepository securityRepository;

        public UnitOfWork(SocialMediaContext _db)
        {
            db = _db;
        }

        public IPostRepository PostRepository => postRepository ?? new PostRepository(db);
        public IRepository<User> UserRepository => userRepository ?? new BaseRepository<User>(db);
        public IRepository<Comment> CommentRepository => commentRepository ?? new BaseRepository<Comment>(db);
        public ISecurityRepository SecurityRepository => securityRepository ?? new SecurityRepository(db);

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
