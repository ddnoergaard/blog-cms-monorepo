using backend_API.DTO.Post;
using backend_API.Models;
using backend_API.Repositories.Interfaces;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Npgsql;
using System.Security.Cryptography.X509Certificates;

namespace backend_API.Repositories
{
    public class PostRepo : IPostRepo
    {
        private readonly string _connectionString;

        public PostRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LocalConnection");
        }

        public async Task<Post?> CreateAsync(CreatePostDTO dto)
        {
            string sqlStatement = "INSERT INTO posts (user_id, title, body, slug, status, cover_image_url) " +
                "VALUES (@userId, @title, @body, @slug, @status, @coverImageUrl) " +
                "RETURNING *";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@userId", dto.UserId);
                    cmd.Parameters.AddWithValue("@title", dto.Title);
                    cmd.Parameters.AddWithValue("@body", dto.Body);
                    cmd.Parameters.AddWithValue("@slug", dto.Slug);
                    cmd.Parameters.AddWithValue("@status", dto.Status);
                    cmd.Parameters.AddWithValue("@coverImageUrl", dto.CoverImageUrl);

                    using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Post
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                UserId = Convert.ToInt32(reader["user_id"]),
                                Title = Convert.ToString(reader["title"]),
                                Body = Convert.ToString(reader["body"]),
                                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                                UpdatedAt = Convert.ToDateTime(reader["updated_at"]),
                                Slug = Convert.ToString(reader["slug"]),
                                Status = Convert.ToString(reader["status"]),
                                PublishedAt = Convert.ToDateTime(reader["published_at"]),
                                CoverImageUrl = Convert.ToString(reader["cover_image_url"])
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            string sqlStatement = "DELETE FROM posts " +
                "WHERE id = @id";


            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    await cmd.ExecuteNonQueryAsync();
                }

            }
        }

        public async Task SetStatusDraft(int id)
        {
            string sqlStatement = "UPDATE posts " +
                "SET status = 'draft' " +
                "WHERE id = @id";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task SetStatusPublished(int id)
        {
            string sqlStatement = "UPDATE posts " +
                "SET status = 'published' " +
                "WHERE id = @id";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task SetStatusArchive(int id)
        {
            string sqlStatement = "UPDATE posts " +
                "SET status = 'archive' " +
                "WHERE id = @id";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    await cmd.ExecuteNonQueryAsync();
                }

            }

        }

        public async Task UpdateCoverImage(UpdateCoverImageUrlDTO dto)
        {
            string sqlStatement = "UPDATE posts " +
                "SET cover_image_url = @coverImageUrl " +
                "WHERE id = @id";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@coverImageUrl", dto.CoverImageUrl);
                    cmd.Parameters.AddWithValue("@id", dto.Id);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateTitle(UpdateTitleDTO dto)
        {
            string sqlStatement = "UPDATE posts " +
                "SET title = @title " +
                "WHERE id = @id";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@title", dto.Title);
                    cmd.Parameters.AddWithValue("@id", dto.Id);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdatePublishedAt(UpdatePublishedAtDTO dto)
        {
            string sqlStatement = "UPDATE posts " +
                "SET published_at = @publishedAt " +
                "WHERE id = @id";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@publishedAt", dto.PublishedAt);
                    cmd.Parameters.AddWithValue("@id", dto.Id);

                    await cmd.ExecuteNonQueryAsync();
                }
            }

        }


    }
}
