using backend_API.DTO.Follower;
using backend_API.DTO.User;
using backend_API.Exceptions;
using backend_API.Repositories.Interfaces;
using backend_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Security.Claims;

namespace backend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFollowService _followService;

        public UserController(IUserService userService, IFollowService followService)
        {
            _userService = userService;
            _followService = followService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UserCreateDTO dto)
        {
            try
            {
                int newId = await _userService.CreateAsync(dto);
                return Created();
            } catch (ConflictException ex)
            {
                return BadRequest(ex.Message);
            } catch (PostgresException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpGet("{publicId}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByExtId(Guid publicId)
        {
            try
            {
                return Ok(await _userService.GetByIdAsync(publicId: publicId));
            } catch (UnexpectedException ex)
            {
                return BadRequest(ex.Message);
            } catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        //FOLLOW SYSTEM
        [Authorize]
        [HttpPost("{userId:guid}/follow")] //this way only an authorized user can follow, and cant manufactur sending another id in the body 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FollowAsync(Guid userId)
        {
            Guid currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (currentUserId == userId)
            {
                return BadRequest("You can't follow yourself");
            }

            try
            {
                await _followService.CreateAsync(userId.ToString(), currentUserId.ToString());
                return Created();
            } catch (UnexpectedException ex)
            {
                return BadRequest("Unexpected.");
            }
        }

        [Authorize]
        [HttpGet("follow")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFollowers()
        {
            Guid currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            List<PublicFollower> publicFollowers = new();

            try
            {
                publicFollowers = (await _followService.GetUserFollowers(currentUserId.ToString())).ToList();
            } catch (UnexpectedException ex)
            {
                return BadRequest(ex.Message);
            }

            if (publicFollowers.Count == 0) return NoContent();

            return Ok(publicFollowers);
        }

        [Authorize]
        [HttpDelete("{userId:guid}/follow")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveFollower(Guid userId)
        {
            Guid currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                await _followService.RemoveAsync(userId.ToString(), currentUserId.ToString());
            } catch (UnexpectedException ex)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
