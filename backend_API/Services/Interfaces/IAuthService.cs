using backend_API.DTO.Auth;

namespace backend_API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAuth(LoginDTO loginDTO);
    }
}
