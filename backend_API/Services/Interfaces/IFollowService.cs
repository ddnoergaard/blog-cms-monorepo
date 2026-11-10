using backend_API.DTO.Follower;

namespace backend_API.Services.Interfaces
{
    public interface IFollowService
    {
        Task CreateAsync(string followingGuid, string followerGuid);
        Task<IEnumerable<PublicFollower>> GetUserFollowers(string guid);
        Task RemoveAsync(string followingGuid, string followerGuid);
    }
}