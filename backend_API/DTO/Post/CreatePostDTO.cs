namespace backend_API.DTO.Post
{
    public class CreatePostDTO
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public string Status { get; set; }
        public string CoverImageUrl { get; set; }

        enum StatusChoices
        {
            draft,
            published,
            archived
        };

    }
}
