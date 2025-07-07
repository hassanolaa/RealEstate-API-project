using realstate.Services.DTOs;

namespace realstate.Services.Repositories.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserDto user);
    }
}
