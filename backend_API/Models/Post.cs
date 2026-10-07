using System.Reflection.Metadata.Ecma335;

namespace backend_API.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; } //fk to who created it
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Slug { get; set; } //The url slug
        public string Status { get; set; } //like in ('draft', 'published', 'archived')
        public DateTime PublishedAt { get; set; }
        public string CoverImageUrl { get; set; }

        enum StatusChoices
        {
            draft,
            published,
            archived
        };

    }
}
