using backend_API.DTO.Post;
using backend_API.Exceptions;
using backend_API.Models;
using backend_API.Repositories;
using backend_API.Repositories.Interfaces;
using backend_API.Services.Interfaces;
using Npgsql;

namespace backend_API.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _postRepo;
        private readonly ILogger _logger;

        public PostService(IPostRepo postRepo, ILogger<PostService> logger)
        {
            _postRepo = postRepo;
            _logger = logger;
        }

        public async Task<Post> CreateAsync(CreatePostDTO dto)
        {
            Post? createdPost = new();

            try
            {
                createdPost = await _postRepo.CreateAsync(dto);
            }
            catch (PostgresException ex)
            {
                _logger.LogError(ex, dto.Title);
                throw new UnexpectedException("Unexpected. Post, CreateAsync. Check logs", ex);
            }

            if (createdPost is null) throw new UnexpectedException("Unexpected. Returned null from db", null);

            return createdPost;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _postRepo.DeleteAsync(id);
            }
            catch (PostgresException ex)
            {
                _logger.LogError(ex, "Unexpected. Post, DeleteAsync");
                throw new UnexpectedException("Unexpected. Post, DeleteAsync. Check logs", null);
            }
        }

        public async Task ChangeStatusAsync(ChangeStatusDTO dto)
        {
            try
            {
                switch (dto.Status)
                {
                    case "draft":
                        await _postRepo.SetStatusDraft(dto.Id);
                        break;
                    case "published":
                        await _postRepo.SetStatusPublished(dto.Id);
                        break;
                    case "archived":
                        await _postRepo.SetStatusArchive(dto.Id);
                        break;
                }
            }
            catch (PostgresException ex)
            {
                _logger.LogError(ex, "Unexpected. Post, Change Status");
                throw new UnexpectedException("Unexpected. Post, Change Status", null);
            }
        }

        public async Task UpdateAsync(UpdateDTO dto)
        {
            dto.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _postRepo.UpdatePostAsync(dto);
            }
            catch (NpgsqlException ex)
            {
                _logger.LogError(ex, "Unexpected while updating post");
                throw new UnexpectedException("Unexpected while updating post", null);
            }
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            List<Post> posts = new();
            try
            {
                posts = (await _postRepo.GetPostsAsync()).ToList();
            } catch (NpgsqlException ex)
            {
                _logger.LogError(ex, "Unexpected while fetching");
                throw new UnexpectedException("Unexpected while fetching", null);
            }

            if (posts.Count == 0) throw new NoPostsFoundException("No posts found.");

            return posts;
        }

        public async Task<Post> GetPostAsync(int id)
        {
            Post? post = new();

            try
            {
                post = await _postRepo.GetPostAsync(id);
            } catch (NpgsqlException ex)
            {
                _logger.LogError(ex, "unexpected, GetPostAsync");
                throw new UnexpectedException("Unexpected, GetPostAsync", ex);
            }

            if (post is null) throw new NoPostFoundException("No post found");

            return post;
        }
    }
}
