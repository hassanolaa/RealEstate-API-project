using AutoMapper;
using realstate.DAL.Models;
using realstate.DAL.Repositories.Interfaces;
using realstate.Helpers;
using realstate.Services.DTOs;
using realstate.Services.Repositories.Interfaces;

namespace realstate.Services.Repositories.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            // Hash the password before mapping
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = PasswordHelper.Hash(dto.Password), // Hash here
                PhoneNumber = dto.PhoneNumber,
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _uow.Users.AddAsync(user);
            await _uow.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _uow.Users.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var entity = await _uow.Users.GetByEmailAsync(email);
            return entity == null ? null : _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var entity = await _uow.Users.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<UserDto>(entity);
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _uow.Users.GetByEmailAsync(email);
            if (user == null || string.IsNullOrEmpty(user.PasswordHash))
                return false;

            // Use PasswordHelper.Verify to check the password
            return PasswordHelper.Verify(user.PasswordHash, password);
        }

        public async Task UpdateAsync(int id, UpdateUserDto dto)
        {
            var entity = await _uow.Users.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("User not found");
            _mapper.Map(dto, entity);
            await _uow.Users.UpdateAsync(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateLastLoginAsync(int userId)
        {
            await _uow.Users.UpdateLastLoginAsync(userId);
            await _uow.SaveChangesAsync();
        }
    }
}
