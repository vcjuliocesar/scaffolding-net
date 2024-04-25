using Scaffolding.Core.Models;
using Scaffolding.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffolding.Core.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(CreateUserDTO user);
        Task<string> LoginAsync(LoginDTO login);
    }
}
