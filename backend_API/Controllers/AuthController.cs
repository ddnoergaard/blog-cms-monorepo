using backend_API.DTO.Auth;
using backend_API.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtHandler _jwtHandler;
        private readonly ILogger _logger;

        public AuthController(JwtHandler jwtHandler, ILogger<AuthController> logger)
        {
            _jwtHandler = jwtHandler;
            _logger = logger;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login ([FromBody] LoginDTO dto)
        {
            if (dto.Username == "admin" && dto.Password == "1234")
            {
                _logger.LogInformation($"Login succesful.: {dto.Username}", dto.Username);
                var token = _jwtHandler.GenerateToken(dto.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

    }
}
