
using Microsoft.AspNetCore.Mvc;
using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using SocialMediaInfraestructure.Repositories;
using System.Threading.Tasks;

namespace Practica1.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository postRepository;

        public PostController(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var post = await postRepository.GetPosts();
            return Ok(post);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await postRepository.GetPost(id);
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Post _post)
        {
            await postRepository.InsertPost(_post);
            return Ok(_post);
        }
    }
}
