using backend_API.DTO.User;
using backend_API.Models;
using backend_API.Repositories.Interfaces;
using Npgsql;

namespace backend_API.Repositories.Moq
{
    public class MoqUserRepo : IUserRepo
    {
        private int _nextId = 3;
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
            User newUser = new User
            {
                Id = ++_nextId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                HashPassword = dto.HashPassword,
                Username = dto.Username,
                PublicId = Guid.NewGuid(),
                IsActive = true
            };

            _users.Add(newUser);
            return newUser.Id;

        }

        public async Task<User?> GetByInternalIdAsync(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<GetPublicUserDTO?> GetByPublicIdAsync(string uuid)
        {
            User internalUser = _users.FirstOrDefault(u => Convert.ToString(u.PublicId) == uuid);

            return new GetPublicUserDTO
            {
                UUID = internalUser.PublicId,
                FirstName = internalUser.FirstName,
                LastName = internalUser.LastName,
                Email = internalUser.Email,
                Username = internalUser.Username
            };
        }

        public async Task UpdateNonEssentialUserData(UpdateNonEssentialUserDataDTO dto, int internalId)
        {
            foreach (User u in _users)
            {
                if (u.Id == internalId)
                {
                    u.FirstName = dto.FirstName;
                    u.LastName = dto.LastName;
                    u.Email = dto.Email;
                    u.Username = dto.Username;
                }
            }
        }

        public async Task UpdatePassword(string password, int internalId)
        {
            _users.FirstOrDefault(u => u.Id == internalId).HashPassword = password;
        }

        public async Task DeleteUserByInteralId(int internalId)
        {
            User userToDelete = _users.FirstOrDefault(u => u.Id == internalId);

            _users.Remove(userToDelete);
        }

        public async Task SetUserInactiveByInternalId(int internalId)
        {
            _users.FirstOrDefault(u => u.Id == internalId).IsActive = false;
        }

        //ESSENTIAL END
        //NON ESSENTIAL START

        public async Task<int> GetInternalIdByPublicId(Guid publicId)
        {
            return _users.FirstOrDefault(u => u.PublicId == publicId).Id;
        }

        public async Task<string?> GetPublicIdByEmail(string email)
        {
            return Convert.ToString(_users.FirstOrDefault(u => u.Email == email).PublicId);
        }

    }
}
