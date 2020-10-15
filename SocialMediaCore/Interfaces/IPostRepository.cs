using SocialMediaCore.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaCore.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(int id);
        Task InsertPost(Post _post);
    }
}
