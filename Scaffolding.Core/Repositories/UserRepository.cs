using Scaffolding.Core.DTOs;
using Scaffolding.Core.Interfaces;
using Scaffolding.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffolding.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
       // private readonly DbContext _context;
        /*public UserRepository(DbContext _context) 
        {
            _context = context;
        }*/
        public User CreateUser(CreateUserDTO userDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
            //return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(int id, UpdateUserDTO userDto)
        {
            throw new NotImplementedException();
        }
    }
}
