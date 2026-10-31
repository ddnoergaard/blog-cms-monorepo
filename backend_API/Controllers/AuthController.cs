using backend_API.DTO.Auth;
using backend_API.DTO.User;
using backend_API.Exceptions;
using backend_API.Models;
using backend_API.Security;
using backend_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtHandler _jwtHandler;
        private readonly ILogger _logger;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;


        public AuthController(JwtHandler jwtHandler, ILogger<AuthController> logger, IAuthService authService, IUserService userService)
        {
            _jwtHandler = jwtHandler;
            _logger = logger;
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login ([FromBody] LoginDTO dto)
        {
            string? token = null;
            try
            {
                token = await _authService.LoginAuth(dto);
            } catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            } catch (NpgsqlException ex)
            {
                return BadRequest(ex.Message);
            }

            Guid? publicId = await _userService.GetPublicIdByEmail(dto.Email);
            int internalId = await _userService.GetInternalIdByPublicId(publicId: publicId.Value);

            User user = await _userService.GetByIdAsync(intId: internalId);

            GetPublicUserDTO publicDto = new GetPublicUserDTO
            {
                UUID = user.PublicId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username
            };

            return token is not null ? Ok(new { Token = token, user = publicDto}) : Unauthorized("Email or password not correct");
        }

    }
}
