using Scaffolding.Services.Auth.Models;
using Scaffolding.Services.Auth.Contracts;
using Scaffolding.Services.Auth.DTOs;
using System.Text;
using System.Security.Cryptography;

namespace Scaffolding.Services.Auth.Services
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

        async Task<UserEntity> IAuthService.RegisterAsync(CreateUserDTO user)
        {
            var existingUser = await _userRepository.GetByEmailAsync(user.Email);

            if (existingUser != null)
            {
                throw new Exception("El usuario ya existe.");
            }

            user.Password = EncryptPassword(user.Password);

            _userRepository.CreateUser(user);

            return new UserEntity
            {
                Name = user.Name,
                Email = user.Email,
            };
        }

        private string GenerateToken(UserEntity user)
        {
            return "token-generado";
        }

        public Task<UserEntity> RegisterAsync(CreateUserDTO user)
        {
            throw new NotImplementedException();
        }

        private string EncryptPassword(string password)
        {
            // Implementa la lógica para encriptar la contraseña aquí
            // Por ejemplo, puedes usar algún algoritmo de hash como SHA-256
            using (var sha256 = SHA256.Create())
            {
                // Convertir la contraseña a bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Calcular el hash
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

                // Convertir el hash a string hexadecimal
                string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hashedPassword;
            }

        }
    }
