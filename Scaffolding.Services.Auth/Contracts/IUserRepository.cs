using Scaffolding.Services.Auth.DTOs;
using Scaffolding.Services.Auth.Models;

namespace Scaffolding.Services.Auth.Contracts
{
    public interface IUserRepository
    {
        // Create
        public void CreateUser(CreateUserDTO userDto);

        // Read
        public UserEntity GetUserById(int id);
        public IEnumerable<UserEntity> GetAllUsers();

        // Update
        public UserEntity UpdateUser(int id, UpdateUserDTO userDto);

        // Delete
        public void DeleteUser(int id);

        // GetByUsernameAsync

        public Task<UserEntity> GetByEmailAsync(string email);
    }
}
