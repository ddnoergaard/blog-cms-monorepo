using backend_API.DTO.Auth;
using backend_API.DTO.User;
using backend_API.Exceptions;
using backend_API.Models;
using backend_API.Security;
using backend_API.Services.Interfaces;
using Npgsql;
using System.Runtime.CompilerServices;

namespace backend_API.Services
{
    public class AuthService
    {
        private readonly JwtHandler _jwtHandler;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly PasswordHasher _passwordHasher;

        public AuthService(ILogger<AuthService> logger, JwtHandler jwtHandler, IUserService userService, PasswordHasher passwordHasher)
        {
            _jwtHandler = jwtHandler;
            _userService = userService;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<string?> LoginAuth(LoginDTO loginDTO)
        {
            string publicId = "";
            try
            {
                publicId = await _userService.GetPublicIdByEmail(loginDTO.Email);
            } catch (NpgsqlException ex)
            {
                throw new UnexpectedException(ex.Message, ex.InnerException);
            } catch (KeyNotFoundException ex)
            {
                throw;
            }

            User user = await _userService.GetByIdAsync(publicId: publicId);

            if (user.Email == loginDTO.Email && _passwordHasher.Verify(loginDTO.Password, user.HashPassword))
            {
                _logger.LogInformation($"Login successful. Email used: {loginDTO.Email}");
                string token = _jwtHandler.GenerateToken(new ClaimsModel
                {
                    PublicId = user.PublicId,
                    Email = user.Email
                });
                return token;
            }
            return null;

        }


    }
}
