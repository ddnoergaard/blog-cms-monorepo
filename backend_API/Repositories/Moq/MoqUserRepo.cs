using backend_API.DTO.User;
using backend_API.Models;
using backend_API.Repositories.Interfaces;
using Npgsql;

namespace backend_API.Repositories.Moq
{
    public class MoqUserRepo : IUserRepo
    {
        private readonly List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Mette",
                LastName = "Hansen",
                Email = "mette.hansen@example.com",
                HashPassword = "hashed_pw_1",
                Username = "mettehansen",
                PublicId = Guid.Parse("a1b2c3d4-0001-4000-8000-000000000001"),
                IsActive = true
            },
            new User
            {
                Id = 2,
                FirstName = "Lars",
                LastName = "Jensen",
                Email = "lars.jensen@example.com",
                HashPassword = "hashed_pw_2",
                Username = "larsj",
                PublicId = Guid.Parse("a1b2c3d4-0002-4000-8000-000000000002"),
                IsActive = true
            },
            new User
            {
                Id = 3,
                FirstName = "Sofie",
                LastName = "Madsen",
                Email = "sofie.madsen@example.com",
                HashPassword = "hashed_pw_3",
                Username = "sofiem",
                PublicId = Guid.Parse("a1b2c3d4-0003-4000-8000-000000000003"),
                IsActive = false
            }
        };

        public MoqUserRepo()
        {
        }

        //ESSENTIAL START

        public async Task<int> CreateAsync(UserCreateDTO dto)
        {
            string sqlStatement = "INSERT INTO users(first_name, last_name, email, hash_password, username) VALUES (@firstName, @lastName, @email, @pw, @username) returning id";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@firstName", dto.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", dto.LastName);
                    cmd.Parameters.AddWithValue("@email", dto.Email);
                    cmd.Parameters.AddWithValue("@pw", dto.HashPassword);
                    cmd.Parameters.AddWithValue("@username", dto.Username);

                    return Convert.ToInt32(await cmd.ExecuteScalarAsync());

                }
            }
        }

        public async Task<User?> GetByInternalIdAsync(int id)
        {
            string sqlQuery = "SELECT * FROM users WHERE id = @id";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                FirstName = Convert.ToString(reader["first_name"]),
                                LastName = Convert.ToString(reader["last_name"]),
                                Email = Convert.ToString(reader["email"]),
                                HashPassword = Convert.ToString(reader["hash_password"]),
                                Username = Convert.ToString(reader["username"]),
                                PublicId = Guid.Parse(Convert.ToString(reader["public_id"])),
                                IsActive = Convert.ToBoolean(reader["is_active"])
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<GetPublicUserDTO?> GetByPublicIdAsync(string uuid)
        {
            string sqlQuery = "SELECT * FROM users WHERE public_id = @uuid";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@uuid", uuid);

                    using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new GetPublicUserDTO
                            {
                                UUID = Guid.Parse(Convert.ToString(reader["public_id"])),
                                FirstName = Convert.ToString(reader["first_name"]),
                                LastName = Convert.ToString(reader["last_name"]),
                                Email = Convert.ToString(reader["email"]),
                                Username = Convert.ToString(reader["username"])
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task UpdateNonEssentialUserData(UpdateNonEssentialUserDataDTO dto, int internalId)
        {
            string sqlStatement = "UPDATE users " +
                "SET first_name = @first, last_name = @last, email = @email, username = @username " +
                "WHERE id = @id";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@first", dto.FirstName);
                    cmd.Parameters.AddWithValue("@last", dto.LastName);
                    cmd.Parameters.AddWithValue("@email", dto.Email);
                    cmd.Parameters.AddWithValue("@username", dto.Username);
                    cmd.Parameters.AddWithValue("@id", internalId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdatePassword(string password, int internalId)
        {
            string sqlStatement = "UPDATE users " +
                "SET hash_password = @h_pw " +
                "WHERE id = @id";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@h_pw", password);
                    cmd.Parameters.AddWithValue("@id", internalId);

                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task DeleteUserByInteralId(int internalId)
        {
            string sqlStatement = "DELETE users " +
                "WHERE id = @id";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@id", internalId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task SetUserInactiveByInternalId(int internalId)
        {
            string sqlStatement = "UPDATE users " +
                "SET is_active = false " +
                "WHERE id = @id";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@id", internalId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }

        }

        //ESSENTIAL END
        //NON ESSENTIAL START

        public async Task<int> GetInternalIdByPublicId(Guid publicId)
        {
            string sqlQuery = "SELECT id FROM users WHERE public_id = @publicId";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@publicId", publicId);
                    return Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }
            }
        }

        public async Task<string?> GetPublicIdByEmail(string email)
        {
            string sqlQuery = "SELECT public_id FROM users WHERE email = @email";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    return Convert.ToString(await cmd.ExecuteScalarAsync());
                }
            }
        }

    }
}
