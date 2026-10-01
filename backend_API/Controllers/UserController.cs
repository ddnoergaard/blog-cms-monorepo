using backend_API.DTO.User;
using backend_API.Exceptions;
using backend_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UserCreateDTO dto)
        {
            try
            {
                int newId = await _userService.CreateAsync(dto);
                return CreatedAtRoute("GetById", new { id = newId }, dto);
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
        public async Task<IActionResult> GetByExtId(string publicId)
        {
            try
            {
                return Ok(await _userService.GetByIdAsync(publicId: publicId));
            } catch (UnexpectedException ex)
            {
                return BadRequest(ex.Message);
            } catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
