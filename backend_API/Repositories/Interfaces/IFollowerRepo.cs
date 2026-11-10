using backend_API.DTO.Follower;
using backend_API.Models;

namespace backend_API.Repositories.Interfaces
{
    public interface IFollowerRepo
    {
        Task CreateAsync(Follow follow);
        Task DeleteAsync(Follow follow);
        Task<IEnumerable<PublicFollower>> GetUserFollowers(int internalId);
    }
}