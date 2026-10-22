using backend_API.DTO.Post;
using backend_API.Models;

namespace backend_API.Services.Interfaces
{
    public interface IPostService
    {
        Task ChangeStatusAsync(ChangeStatusDTO dto);
        Task<Post> CreateAsync(CreatePostDTO dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateDTO dto);
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<Post> GetPostAsync(int id);
    }
}