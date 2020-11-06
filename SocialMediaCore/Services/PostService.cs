using Microsoft.Extensions.Options;
using SocialMediaCore.Entidades;
using SocialMediaCore.Entidades.CustomEntities;
using SocialMediaCore.Entidades.QueryFilters;
using SocialMediaCore.Exceptions;
using SocialMediaCore.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaCore.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly PaginationOptions paginationOptions;

        public PostService(IUnitOfWork _unitOfWork, IOptions<PaginationOptions> options)
        {
            unitOfWork = _unitOfWork;
            paginationOptions = options.Value;
        }

        public async Task<Post> GetPost(int id)
        {
            return await unitOfWork.PostRepository.GetById(id);
        }

        public PageList<Post> GetPosts(PostQueryFilters filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? paginationOptions.DefaultPageSize : filters.PageSize;

            var posts = unitOfWork.PostRepository.GetAll();
            if (filters.UserId != null)
            {
                posts = posts.Where(x => x.UserId == filters.UserId);
            }
            if (filters.Date != null)
            {
                posts = posts.Where(x => x.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            }
            if (filters.Description != null)
            {
                posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            var pagedPost = PageList<Post>.Create(posts,filters.PageNumber, filters.PageSize);

            return pagedPost;
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
            var existingPost = await unitOfWork.PostRepository.GetById(post.Id);
            existingPost.Image = post.Image;
            existingPost.Description = post.Description;

            unitOfWork.PostRepository.Update(existingPost);
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
