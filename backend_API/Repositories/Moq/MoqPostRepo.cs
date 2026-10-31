using backend_API.DTO.Post;
using backend_API.Models;
using backend_API.Repositories.Interfaces;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Npgsql;
using System.Security.Cryptography.X509Certificates;

namespace backend_API.Repositories.Moq
{
    public class MoqPostRepo : IPostRepo
    {
        private int _nextId = 5;

        private readonly List<Post> _posts = new List<Post>
        {
            new Post
            {
                Id = 1,
                UserId = 1,
                Title = "Getting Started with .NET",
                Body = "A short intro to building your first backend API with C# and .NET.",
                CreatedAt = new DateTime(2026, 10, 1, 9, 0, 0),
                UpdatedAt = new DateTime(2026, 10, 2, 14, 30, 0),
                Slug = "getting-started-with-dotnet",
                Status = "published",
                PublishedAt = new DateTime(2026, 10, 2, 15, 0, 0),
                CoverImageUrl = "https://example.com/images/dotnet.jpg"
            },
            new Post
            {
                Id = 2,
                UserId = 1,
                Title = "Why I Love Dependency Injection",
                Body = "Notes on how DI makes testing and swapping implementations easier.",
                CreatedAt = new DateTime(2026, 10, 5, 11, 15, 0),
                UpdatedAt = new DateTime(2026, 10, 5, 11, 15, 0),
                Slug = "why-i-love-dependency-injection",
                Status = "draft",
                PublishedAt = default,
                CoverImageUrl = "https://example.com/images/di.jpg"
            },
            new Post
            {
                Id = 3,
                UserId = 2,
                Title = "A Cozy Evening Reading List",
                Body = "Five books I keep coming back to when the weather turns.",
                CreatedAt = new DateTime(2026, 9, 20, 18, 0, 0),
                UpdatedAt = new DateTime(2026, 9, 22, 8, 45, 0),
                Slug = "cozy-evening-reading-list",
                Status = "published",
                PublishedAt = new DateTime(2026, 9, 22, 9, 0, 0),
                CoverImageUrl = "https://example.com/images/books.jpg"
            },
            new Post
            {
                Id = 4,
                UserId = 2,
                Title = "Old Post About Cron Jobs",
                Body = "An outdated guide I no longer stand behind.",
                CreatedAt = new DateTime(2026, 6, 3, 10, 0, 0),
                UpdatedAt = new DateTime(2026, 8, 1, 12, 0, 0),
                Slug = "old-post-about-cron-jobs",
                Status = "archived",
                PublishedAt = new DateTime(2026, 6, 4, 8, 0, 0),
                CoverImageUrl = "https://example.com/images/cron.jpg"
            },
            new Post
            {
                Id = 5,
                UserId = 3,
                Title = "Weekend Hiking Notes",
                Body = "Trail conditions, gear, and a few photos from the weekend.",
                CreatedAt = new DateTime(2026, 10, 18, 20, 30, 0),
                UpdatedAt = new DateTime(2026, 10, 19, 7, 0, 0),
                Slug = "weekend-hiking-notes",
                Status = "published",
                PublishedAt = new DateTime(2026, 10, 19, 8, 0, 0),
                CoverImageUrl = "https://example.com/images/hiking.jpg"
            }
        };


        public MoqPostRepo()
        {
        }

        public async Task<Post?> CreateAsync(CreatePostDTO dto)
        {
            Post newPost = new Post
            {
                Id = ++_nextId,
                UserId = dto.UserId,
                Title = dto.Title,
                Body = dto.Body,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Slug = dto.Slug,
                Status = dto.Status,
                PublishedAt = DateTime.UtcNow,
                CoverImageUrl = dto.CoverImageUrl
            };

            _posts.Add(newPost);

            return newPost;

        }

        public async Task DeleteAsync(int id)
        {
            Post? postToDelete = _posts.FirstOrDefault(p => p.Id == id);

            _posts.Remove(postToDelete);
        }

        public async Task SetStatusDraft(int id)
        {
            _posts.FirstOrDefault(p => p.Id == id).Status = "draft";
        }

        public async Task SetStatusPublished(int id)
        {
            _posts.FirstOrDefault(p => p.Id == id).Status = "published";
        }

        public async Task SetStatusArchive(int id)
        {
            _posts.FirstOrDefault(p => p.Id == id).Status = "archive";
        }

        public async Task UpdateCoverImage(UpdateCoverImageUrlDTO dto)
        {
            _posts.FirstOrDefault(p => p.Id == dto.Id).CoverImageUrl = dto.CoverImageUrl;
        }

        public async Task UpdateTitle(UpdateTitleDTO dto)
        {
            _posts.FirstOrDefault(p => p.Id == dto.Id).Title = dto.Title;
        }

        public async Task UpdatePublishedAt(UpdatePublishedAtDTO dto)
        {
            _posts.FirstOrDefault(p => p.Id == dto.Id).PublishedAt = dto.PublishedAt;
        }

        public async Task UpdatePostAsync(UpdateDTO dto)
        {
            foreach (Post p in _posts)
            {
                if (p.Id == dto.Id)
                {
                    p.Title = dto.Title;
                    p.Body = dto.Body;
                    p.Slug = dto.Slug;
                    p.CoverImageUrl = dto.CoverImageUrl;
                    p.UpdatedAt = dto.UpdatedAt;
                }
            }
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return new List<Post>(_posts);
        }

        public async Task<Post?> GetPostAsync(int id)
        {
            return _posts.FirstOrDefault(p => p.Id == id);
        }


    }
}
