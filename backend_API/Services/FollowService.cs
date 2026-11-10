using backend_API.DTO.Follower;
using backend_API.Exceptions;
using backend_API.Models;
using backend_API.Repositories.Interfaces;
using backend_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend_API.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowerRepo _followerRepo;
        private readonly ILogger<FollowService> _logger;
        private readonly IUserService _userService;

        public FollowService(IFollowerRepo followerRepo, ILogger<FollowService> logger, IUserService userService)
        {
            _followerRepo = followerRepo;
            _logger = logger;
            _userService = userService;
        }

        public async Task CreateAsync(string followingGuid, string followerGuid)
        {
            int followingInternalId = await _userService.GetInternalIdByPublicId(Guid.Parse(followingGuid));
            int followerInternalId = await _userService.GetInternalIdByPublicId(Guid.Parse(followerGuid));

            Follow newFollow = new()
            {
                FollowerId = followerInternalId,
                FollowingId = followingInternalId
            };

            try
            {
                await _followerRepo.CreateAsync(newFollow);
            }
            catch (NpgsqlException ex)
            {
                _logger.LogError("Npg expection thrown by db", newFollow);
                throw new UnexpectedException("Something unexpected happened", ex);
            }
        }

        public async Task<IEnumerable<PublicFollower>> GetUserFollowers(string guid)
        {
            int internalId = await _userService.GetInternalIdByPublicId(Guid.Parse(guid));

            List<PublicFollower> publicFollowers = new();

            try
            {
                publicFollowers = (await _followerRepo.GetUserFollowers(internalId)).ToList();
                return publicFollowers;
            }
            catch (NpgsqlException ex)
            {
                _logger.LogError("Npg exception thrown by db", guid);
                throw new UnexpectedException("Something unexpected happened", ex);
            }
        }

        public async Task RemoveAsync(string followingGuid, string followerGuid)
        {
            int internalFollowingGuid = await _userService.GetInternalIdByPublicId(Guid.Parse(followingGuid));
            int internalFollowerGuid = await _userService.GetInternalIdByPublicId(Guid.Parse(followerGuid));

            Follow followToRemove = new()
            {
                FollowingId = internalFollowingGuid,
                FollowerId = internalFollowerGuid
            };

            try
            {
                await _followerRepo.DeleteAsync(followToRemove);
            }
            catch (NpgsqlException ex)
            {
                _logger.LogError("Npg exception thrown by db", followToRemove);
                throw new UnexpectedException("Something unexpected happened", ex);
            }
        }

    }
}
