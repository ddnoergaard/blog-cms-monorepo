using Npgsql.Replication.PgOutput;

namespace backend_API.Models
{
    public class ClaimsModel
    {
        public Guid PublicId { get; set; }
        public string Email { get; set; }
    }
}
