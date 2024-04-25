using Scaffolding.Core.DTOs;
using Scaffolding.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffolding.Core.Interfaces
{
    public interface IUserRepository
    {
        // Create
        public User CreateUser(CreateUserDTO userDto);

        // Read
        public User GetUserById(int id);
        public IEnumerable<User> GetAllUsers();

        // Update
        public User UpdateUser(int id, UpdateUserDTO userDto);

        // Delete
        public void DeleteUser(int id);
    }
}
