using SocialMediaCore.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaCore.Interfaces
{
    public interface IPostService
    {
        IEnumerable<Post> GetPosts();
        Task<Post> GetPost(int id);
        Task InsertPostAsync(Post _post);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeleteAsync(int id);
    }
}