using backend_API.DTO.User;
using backend_API.Models;

namespace backend_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateAsync(UserCreateDTO dto);
        Task<User> GetByIdAsync(int? intId = null, Guid? publicId = null);
        //Task<GetPublicUserDTO> GetByPublicIdAsync(string uuid);
        Task<int> GetInternalIdByPublicId(Guid publicId);
        Task SetUserInactiveByInteralId(Guid uuid);
        Task UpdateNonEssentialUserData(UpdateNonEssentialUserDataDTO dto, Guid publicId);
        Task UpdatePassword(string password, Guid uuid);
        Task<Guid?> GetPublicIdByEmail(string email);
    }
}