using backend_API.DTO.Post;
using backend_API.Models;

namespace backend_API.Repositories.Interfaces
{
    public interface IPostRepo
    {
        Task<Post?> CreateAsync(CreatePostDTO dto);
        Task DeleteAsync(int id);
        Task SetStatusArchive(int id);
        Task SetStatusDraft(int id);
        Task SetStatusPublished(int id);
        Task UpdateCoverImage(UpdateCoverImageUrlDTO dto);
        Task UpdatePublishedAt(UpdatePublishedAtDTO dto);
        Task UpdateTitle(UpdateTitleDTO dto);
    }
}