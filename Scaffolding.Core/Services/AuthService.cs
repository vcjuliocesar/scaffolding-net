using Scaffolding.Core.DTOs;
using Scaffolding.Core.Interfaces;
using Scaffolding.Core.Models;
using Scaffolding.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scaffolding.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        async Task<string> IAuthService.LoginAsync(LoginDTO login)
        {
            var user = await _userRepository.GetByEmailAsync(login.Email);

            if (user == null || user.Password != login.Password)
            {
                throw new Exception("Credenciales inválidas");
            }

            return GenerateToken(user);
        }

        async Task<User> IAuthService.RegisterAsync(CreateUserDTO user)
        {
            var existingUser = await _userRepository.GetByEmailAsync(user.Email);

            if(existingUser != null) 
            {
                throw new Exception("El usuario ya existe.");
            }

            _userRepository.CreateUser(user);
            
            return new User
            {
                Name = user.Name,
                Email = user.Email,
            };
        }

        private string GenerateToken(User user)
        {
            return "token-generado";
        }
    }
}
