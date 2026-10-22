using backend_API.DTO.Post;
using backend_API.Exceptions;
using backend_API.Models;
using backend_API.Repositories.Interfaces;
using backend_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePostDTO dTO)
        {
            Post post = new();
            try
            {
                post = await _postService.CreateAsync(dTO);
                return Ok(post);
            } catch (UnexpectedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPosts(int? id = null)
        {
            if (id is not null)
            {
                try
                {
                    Post post = await _postService.GetPostAsync(id.Value);
                } catch (UnexpectedException ex)
                {
                    return BadRequest(ex.Message);
                } catch (NoPostFoundException)
                {
                    return NoContent();
                }
            }


            try
            {
                List<Post> posts = (await _postService.GetPostsAsync()).ToList();
                return Ok(posts);
            } catch (UnexpectedException ex)
            {
                return BadRequest(ex.Message);
            } catch (NoPostsFoundException)
            {
                return NoContent();
            }
        }

    }
}
