using SocialMediaCore.Entidades;
using SocialMediaCore.Exceptions;
using SocialMediaCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Post> GetPosts()
        {
            return unitOfWork.PostRepository.GetAll();
        }

        public async Task InsertPostAsync(Post post)
        {
            var user = await unitOfWork.UserRepository.GetById(post.UserId);
            if(user == null)
            {
                throw new BusinessException("Usuario no existe");
            }

            var userPost = await unitOfWork.PostRepository.GetPostsByUser(post.UserId);
            if (userPost.Count() < 10)
            {
                var lastPost = userPost.OrderByDescending(x=>x.Date).FirstOrDefault();
                if((DateTime.Now - lastPost.Date).TotalDays < 7)
                {
                    throw new BusinessException("No tiene permitido temporalmente publicar");
                }
            }

            if (post.Description.Contains("sexo"))
            {
                throw new BusinessException("Contenido no permitido");
            }
            await unitOfWork.PostRepository.Add(post);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            unitOfWork.PostRepository.Update(post);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await unitOfWork.PostRepository.Delete(id);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
