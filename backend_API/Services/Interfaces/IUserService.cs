using backend_API.DTO.User;
using backend_API.Models;

namespace backend_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateAsync(UserCreateDTO dto);
        Task<User> GetByIdAsync(int? intId = null, string? publicId = null);
        //Task<GetPublicUserDTO> GetByPublicIdAsync(string uuid);
        Task<int> GetInternalIdByPublicId(string uuid);
        Task SetUserInactiveByInteralId(string uuid);
        Task UpdateNonEssentialUserData(UpdateNonEssentialUserDataDTO dto, string uuid);
        Task UpdatePassword(string password, string uuid);
        Task<string> GetPublicIdByEmail(string email);
    }
}