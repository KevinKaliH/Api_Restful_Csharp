
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SocialMediaCore.DTOs;
using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using SocialMediaInfraestructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practica1.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public PostController(IPostRepository _postRepository, IMapper _mapper)
        {
            postRepository = _postRepository;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await postRepository.GetPosts();

            //con mapper
            var postsDto = mapper.Map<IEnumerable<PostDTO>>(posts);

            //sin mapper
            //var posts = await postRepository.GetPosts();
            //var postsDto = posts.Select(x => new PostDTO
            //{
            //    PostId = x.PostId,
            //    Date = x.Date,
            //    Description = x.Description,
            //    Image = x.Image,
            //    UserId = x.UserId
            //});

            return Ok(postsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await postRepository.GetPost(id);
            var postDto = mapper.Map<PostDTO>(post);

            return Ok(postDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDTO _post)
        {
            var post = mapper.Map<Post>(_post);

            await postRepository.InsertPost(post);
            return Ok(post);
        }
    }
}
