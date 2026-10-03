using backend_API.DTO.User;
using backend_API.Models;
using backend_API.Repositories.Interfaces;
using Npgsql;

namespace backend_API.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly string _connectionString;


        public UserRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LocalConnection");
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
                                PublicId = Convert.ToString(reader["public_id"]),
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
                                UUID = Convert.ToString(reader["public_id"]),
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

        public async Task<int> GetInternalIdByPublicId(string publicId)
        {
            string sqlQuery = "SELECT id FROM users WHERE public_id = @publicId";

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con))
                {
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
