using Scaffolding.Services.Auth.Models;
using System.Data;
using Scaffolding.Services.Auth.Contracts;
using Scaffolding.Services.Auth.DTOs;

namespace Scaffolding.Services.Auth.Repositories
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
                _context.ExecuteQuery(query, CommandType.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally { _context.Close(); }
        }


        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserEntity GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public UserEntity UpdateUser(int id, UpdateUserDTO userDto)
        {
            throw new NotImplementedException();
        }

        async Task<UserEntity> IUserRepository.GetByEmailAsync(string email)
        {
            UserEntity user = null;
            try
            {
                _context.Open();
                string query = $"SELECT Id, Name, Email, Password, CreatedAt FROM Users WHERE Email = '{email}'";
                var selectResults = _context.ExecuteQuery(query);

                if (selectResults.Count > 0)
                {
                    var userResult = selectResults[0];

                    user = new UserEntity
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
            }
            finally
            {
                _context.Close();
            }

            return user;
            //return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /*public UserEntity GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public UserEntity UpdateUser(int id, UpdateUserDTO userDto)
        {
            throw new NotImplementedException();
        }


        IEnumerable<UserEntity> IUserRepository.GetAllUsers()
        {
            throw new NotImplementedException();
        }

        Task<UserEntity> IUserRepository.GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        UserEntity IUserRepository.GetUserById(int id)
        {
            throw new NotImplementedException();
        }*/
    }
}
