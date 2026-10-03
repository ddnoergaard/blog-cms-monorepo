using backend_API.DTO.Auth;
using backend_API.Exceptions;
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

        public AuthController(JwtHandler jwtHandler, ILogger<AuthController> logger, IAuthService authService)
        {
            _jwtHandler = jwtHandler;
            _logger = logger;
            _authService = authService;
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

            return token is not null ? Ok(new { Token = token }) : Unauthorized();
        }

    }
}
