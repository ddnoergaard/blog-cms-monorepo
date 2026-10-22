using Npgsql.Replication.PgOutput;
using System.Reflection.Metadata.Ecma335;

namespace backend_API.DTO.Post
{
    public class UpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
