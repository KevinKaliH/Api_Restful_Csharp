using SocialMediaCore.Entidades;
using SocialMediaCore.Entidades.CustomEntities;
using SocialMediaCore.Entidades.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaCore.Interfaces
{
    public interface IPostService
    {
        PageList<Post> GetPosts(PostQueryFilters filters);
        Task<Post> GetPost(int id);
        Task InsertPostAsync(Post _post);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeleteAsync(int id);
    }
}