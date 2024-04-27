using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Scaffolding.Core.DTOs;
using Scaffolding.Core.Interfaces;
using Scaffolding.Core.Models;
namespace Scaffolding.Test
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void CreateUser_Should_Return_New_User()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var userDto = new CreateUserDTO
            {
                Name = "John Doe",
                Email = "john@example.com",
                Password = "password123"
            };
            var newUser = new User // Crear un usuario simulado para devolver
            {
                Id = 1,
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                CreatedAt = DateTime.Now // Puedes establecer la fecha actual o una específica para CreatedAt
            };
           // mockRepository.Setup(repo => repo.CreateUser(userDto)).Returns(newUser);

            // Act
            //var createdUser = mockRepository.Object.CreateUser(userDto);

            // Assert
            //Assert.IsNotNull(createdUser);
            //Assert.AreEqual(userDto.Name, createdUser.Name);
            //Assert.AreEqual(userDto.Email, createdUser.Email);
            //Assert.AreEqual(userDto.Password, createdUser.Password);
            // Verifica que CreatedAt no sea nulo
            //Assert.IsNotNull(createdUser.CreatedAt);
        }

        public void UpdateUser_Should_Update_User()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var userDto = new UpdateUserDTO
            {
                Email = "john@example2.com",
            };
            var newUser = new User // Crear un usuario simulado para devolver
            {
                Id = 1,
                Name = "John Doe",
                Email = userDto.Email,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now // Puedes establecer la fecha actual o una específica para CreatedAt
            };
            mockRepository.Setup(repo => repo.UpdateUser(1,userDto)).Returns(newUser);

            // Act
            var createdUser = mockRepository.Object.UpdateUser(1, userDto);

            // Assert
            Assert.IsNotNull(createdUser);
            Assert.AreEqual(userDto.Name, createdUser.Name);
            Assert.AreEqual(userDto.Email, createdUser.Email);
            Assert.AreEqual(userDto.Password, createdUser.Password);
            // Verifica que CreatedAt no sea nulo
            Assert.IsNotNull(createdUser.UpdatedAt);
        }
    }
}