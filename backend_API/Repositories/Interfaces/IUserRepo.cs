using backend_API.DTO.User;
using backend_API.Models;

namespace backend_API.Repositories.Interfaces
{
    public interface IUserRepo
    {
        Task<int> CreateAsync(UserCreateDTO dto);
        Task DeleteUserByInteralId(int internalId);
        Task<User?> GetByInternalIdAsync(int id);
        Task<GetPublicUserDTO?> GetByPublicIdAsync(string uuid);
        Task<int> GetInternalIdByPublicId(string publicId);
        Task SetUserInactiveByInternalId(int internalId);
        Task UpdateNonEssentialUserData(UpdateNonEssentialUserDataDTO dto, int internalId);
        Task UpdatePassword(string password, int internalId);
        Task<string?> GetPublicIdByEmail(string email);
    }
}