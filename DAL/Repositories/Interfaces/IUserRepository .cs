using realstate.DAL.Models;

namespace realstate.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ValidateCredentialsAsync(string email, string passwordHash);
        Task UpdateLastLoginAsync(int userId);
    }
}
