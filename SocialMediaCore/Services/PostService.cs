using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaCore.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;

        public PostService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<Post> GetPost(int id)
        {
            return await unitOfWork.PostRepository.GetById(id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await unitOfWork.PostRepository.GetAll();
        }

        public async Task InsertPostAsync(Post post)
        {
            var user = await unitOfWork.UserRepository.GetById(post.UserId);
            if(user == null)
            {
                throw new Exception("Usuario no existe");
            }

            if (post.Description.Contains("sexo"))
            {
                throw new Exception("Contenido no permitido");
            }
            await unitOfWork.PostRepository.Add(post);
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            await unitOfWork.PostRepository.Update(post);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await unitOfWork.PostRepository.Delete(id);
            return true;
        }
    }
}
