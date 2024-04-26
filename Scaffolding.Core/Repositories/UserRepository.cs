using Scaffolding.Core.DTOs;
using Scaffolding.Core.Interfaces;
using Scaffolding.Core.Models;
using Scaffolding.Dbcontext.Dbcontext;
using Scaffolding.Dbcontext.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Scaffolding.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseConnection _context;
        public UserRepository(IDatabaseConnection context) 
        {
            _context = context;
        }
        public void CreateUser(CreateUserDTO userDto)
        {
            try
            {
                _context.Open();
                string query = $"INSERT INTO Users (Name, Email, Password, CreatedAt) " +
                           $"VALUES ('{userDto.Name}', '{userDto.Email}', '{userDto.Password}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
                _context.ExecuteQuery(query,CommandType.Text);
            }
            catch (Exception ex) {
               Console.WriteLine(ex.ToString());
            }finally { _context.Close(); }
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            User user = null;
            try
            {
                _context.Open();
                string query = $"SELECT Id, Name, Email, Password, CreatedAt FROM Users WHERE Email = '{email}'";
                var selectResults = _context.ExecuteQuery(query);
                
                if(selectResults.Count > 0)
                {
                    var userResult = selectResults[0];

                    user = new User
                    {
                        Id = userResult.Id,
                        Name = userResult.Name,
                        Email = userResult.Email,
                        Password = userResult.Password,
                        CreatedAt = userResult.CreatedAt
                    };
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }finally 
            { 
                _context.Close(); 
            }

            return user;
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
