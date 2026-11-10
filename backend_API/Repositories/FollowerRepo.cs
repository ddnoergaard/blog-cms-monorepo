using backend_API.DTO.Follower;
using backend_API.Models;
using backend_API.Repositories.Interfaces;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Npgsql;
using System.Runtime.CompilerServices;

namespace backend_API.Repositories
{
    public class FollowerRepo : IFollowerRepo
    {
        private readonly string _conString;

        public FollowerRepo(IConfiguration configuration)
        {
            _conString = configuration.GetConnectionString("LocalConnection");
        }

        public async Task CreateAsync(Follow follow)
        {
            string sqlStatement = "INSERT INTO followers(following_id, follower_id) " +
                "VALUES (@following, @follower)";

            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@following", follow.FollowingId);
                    cmd.Parameters.AddWithValue("@follower", follow.FollowerId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }

        }

        public async Task<IEnumerable<PublicFollower>> GetUserFollowers(int internalId)
        {
            string sqlQuery = "SELECT users.username, users.email, followers.created_at " +
                "FROM followers " +
                "FULL JOIN users " +
                "ON users.id=folowers.follower_id " +
                "WHERE followers.follower_id = @followerId";

            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@followerId", internalId);

                    using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        List<PublicFollower> returnList = new();

                        while (await reader.ReadAsync())
                        {
                            returnList.Add(new PublicFollower
                            {
                                Username = Convert.ToString(reader["username"]),
                                Email = Convert.ToString(reader["email"]),
                                CreatedAt = Convert.ToDateTime(reader["created_at"])
                            });
                        }
                        return returnList;
                    }
                }
            }
        }

        public async Task DeleteAsync(Follow follow)
        {
            string sqlStatement = "DELETE FROM followers " +
                "WHERE following_id = @followingId AND follower_id = @followerId";

            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                await con.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@followingId", follow.FollowingId);
                    cmd.Parameters.AddWithValue("@followerId", follow.FollowerId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
