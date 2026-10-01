using backend_API.DTO.User;
using backend_API.Exceptions;
using backend_API.Repositories.Interfaces;
using backend_API.Security;
using backend_API.Services.Interfaces;
using Npgsql;

namespace backend_API.Services
{
    public class UserService : IUserService
    {
        private IUserRepo _userRepo;
        private ILogger _logger;
        private PasswordHasher _passwordHasher;

        public UserService(ILogger logger, IUserRepo userRepo, PasswordHasher passwordHasher)
        {
            _logger = logger;
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
        }

        //ESSENTIAL START
        public async Task<int> CreateAsync(UserCreateDTO dto) //PASSWORD HASHING MANGLER
        {
            //Password hashing
            string hashedPassword = _passwordHasher.Hash(dto.HashPassword); //SAVES THE HASHED PASSWORD IN A VAR
            dto.HashPassword = hashedPassword; //OVERRIDES THE PLAIN PASSWORD WITH THE HASHED
            //-------
            try
            {
                return await _userRepo.CreateAsync(dto);
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                _logger.LogWarning(ex, dto.Email);
                throw new ConflictException("Failed to create. Try again", ex.InnerException);
            }
            catch (PostgresException ex) when (ex.IsTransient) //TRANSIENT MEANS "could this exact same operation succeed if i just try again?"
            {
                _logger.LogError(ex, "Unexpected DB error creating user");
                throw;
            }
        }

        /// <summary>
        /// Takes either internal id, or public it. Public id gets converted into internal id
        /// </summary>
        public async Task<GetPublicUserDTO> GetByIdAsync(int? intId = null, string? publicId = null)
        {
            int idToUse = intId is not null ? intId.Value : await _userRepo.GetInternalIdByPublicId(publicId);

            GetPublicUserDTO? dto = new();
            try
            {
                dto = await _userRepo.GetByInternalIdAsync(idToUse);
            }
            catch (PostgresException ex)
            {
                _logger.LogCritical(ex, "Something unexpected happened");
                throw new UnexpectedException("Something unexpected happened. Check logs", ex.InnerException);
            }

            return dto is null ? throw new UserNotFoundException("User not found") : dto;
        }

        //public async Task<GetPublicUserDTO> GetByPublicIdAsync(string uuid)
        //{
        //    GetPublicUserDTO? dto = new();
        //    try
        //    {
        //        dto = await _userRepo.GetByPublicIdAsync(uuid);
        //    }
        //    catch (PostgresException ex)
        //    {
        //        _logger.LogCritical(ex, "Something unexpected happened");
        //        throw new UnexpectedException("Something unexpected happened. Check logs.", ex.InnerException);
        //    }

        //    return dto is null ? throw new UserNotFoundException("User not found") : dto;
        //}

        public async Task<int> GetInternalIdByPublicId(string uuid)
        {
            try
            {
                return await _userRepo.GetInternalIdByPublicId(uuid);
            }
            catch (NpgsqlException ex)
            {
                _logger.LogCritical(ex, "Something unexpected happened");
                throw new UnexpectedException("Something unexpected happened. Check logs.", ex.InnerException);
            }
        }


        public async Task UpdateNonEssentialUserData(UpdateNonEssentialUserDataDTO dto, string uuid)
        {
            int internalId = 0;
            try
            {
                internalId = await GetInternalIdByPublicId(uuid);
            }
            catch { throw; }

            try
            {
                await _userRepo.UpdateNonEssentialUserData(dto, internalId);
            }
            catch (NpgsqlException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                _logger.LogWarning(ex, dto.Email);
                throw new ConflictException("Failed to create. Try again", ex.InnerException);
            }
            catch (NpgsqlException ex)
            {
                _logger.LogCritical(ex, "Something unexpected happened");
                throw new UnexpectedException("Something unexpected happened. Check logs.", ex.InnerException);
            }
        }

        public async Task UpdatePassword(string password, string uuid)
        {
            int internalId = 0;
            try
            {
                internalId = await GetInternalIdByPublicId(uuid);
            }
            catch { throw; }

            string passwordHash = _passwordHasher.Hash(password);

            try
            {
                await _userRepo.UpdatePassword(passwordHash, internalId);
            }
            catch (NpgsqlException ex)
            {
                _logger.LogCritical(ex, "Something unexpected happened");
                throw new UnexpectedException("Something unexpected happened. Check logs.", ex.InnerException);
            }
        }

        public async Task SetUserInactiveByInteralId(string uuid)
        {
            int interalId = 0;
            try
            {
                interalId = await GetInternalIdByPublicId(uuid);
            }
            catch { throw; }

            try
            {
                await _userRepo.SetUserInactiveByInternalId(interalId);
            }
            catch (NpgsqlException ex)
            {
                _logger.LogCritical(ex, "Something unexpected happened");
                throw new UnexpectedException("Something unexpected happened. Check logs.", ex.InnerException);
            }
        }
    }
}
