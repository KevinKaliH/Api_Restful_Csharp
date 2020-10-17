using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaCore.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IUserRepository userRepository;

        public PostService(IPostRepository _postRepository, IUserRepository _userRepository)
        {
            postRepository = _postRepository;
            userRepository = _userRepository;
        }

        public async Task<Post> GetPost(int id)
        {
            return await postRepository.GetPost(id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await postRepository.GetPosts();
        }

        public async Task InsertPostAsync(Post post)
        {
            var user = await userRepository.GetUser(post.UserId);
            if(user == null)
            {
                throw new Exception("Usuario no existe");
            }

            if (post.Description.Contains("sexo"))
            {
                throw new Exception("Contenido no permitido");
            }
            await postRepository.InsertPost(post);
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            return await postRepository.UpdatePostAsync(post);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await postRepository.DeleteAsync(id);
        }
    }
}
