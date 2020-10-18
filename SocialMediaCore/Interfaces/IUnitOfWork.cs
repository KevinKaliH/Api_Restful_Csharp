using SocialMediaCore.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Post> PostRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
} 
