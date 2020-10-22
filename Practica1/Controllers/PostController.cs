
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Practica1.Responses;
using SocialMediaCore.DTOs;
using SocialMediaCore.Entidades;
using SocialMediaCore.Entidades.CustomEntities;
using SocialMediaCore.Entidades.QueryFilters;
using SocialMediaCore.Interfaces;
using SocialMediaInfraestructure.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;

namespace Practica1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly IMapper mapper;
        private readonly IUriService uriService;

        public PostController(IPostService _postService, IMapper _mapper, IUriService _uriService)
        {
            postService = _postService;
            mapper = _mapper;
            uriService = _uriService;
        }

        /// <summary>
        /// Retrieve all posts
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPosts))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PostDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPosts([FromQuery]PostQueryFilters filters)  //recibiendo parametros y decorandolo en una entidad
        {
            var posts = postService.GetPosts(filters);

            var postsDto = mapper.Map<IEnumerable<PostDTO>>(posts);

            var metadata = new Metadata
            {
                TotalCount = posts.TotalCount,
                PageSize = posts.PageSize,
                CurrentPage = posts.CurrrentPage,
                TotalPage = posts.TotalPages,
                HasNextPage = posts.HasNextPage,
                HasPreviousPage = posts.HasPreviousPage,
                NextPageUrl = uriService.GetPostPagination(filters, Url.RouteUrl(nameof(GetPost))).ToString(),
                PreviousPageUrl = uriService.GetPostPagination(filters, Url.RouteUrl(nameof(GetPost))).ToString()
            };

            var response = new ApiResponse<IEnumerable<PostDTO>>(postsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("x-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await postService.GetPost(id);
            var postDto = mapper.Map<PostDTO>(post);
            var response = new ApiResponse<PostDTO>(postDto);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostDTO _post)
        {
            var post = mapper.Map<Post>(_post);

            await postService.InsertPostAsync(post);
            var postDto = mapper.Map<PostDTO>(post);

            var response = new ApiResponse<PostDTO>(postDto);
            return Ok(response);
        }
    
        [HttpPut]
        public async Task<IActionResult> Put(int id, PostDTO _post)
        {
            var post = mapper.Map<Post>(_post);
            post.Id = id;

            var result = await postService.UpdatePostAsync(post);
            var response = new ApiResponse<bool>(result);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await postService.DeleteAsync(id);
            var response = new ApiResponse<bool>(result);

            return Ok(response);
        }
    }
}
