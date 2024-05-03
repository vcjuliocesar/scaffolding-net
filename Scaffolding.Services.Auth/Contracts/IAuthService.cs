using Scaffolding.Services.Auth.Models;
using Scaffolding.Services.Auth.DTOs;

namespace Scaffolding.Services.Auth.Contracts
{
    public interface IAuthService
    {
        Task<UserEntity> RegisterAsync(CreateUserDTO user);
        Task<string> LoginAsync(LoginDTO login);
    }
}
