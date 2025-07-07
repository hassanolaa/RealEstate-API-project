using realstate.Services.DTOs;

namespace realstate.Services.Repositories.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto?> GetByEmailAsync(string email);
        Task UpdateAsync(int id, UpdateUserDto dto);
        Task DeleteAsync(int id);
        Task<bool> ValidateCredentialsAsync(string email, string password);
        Task UpdateLastLoginAsync(int userId);
    }


}