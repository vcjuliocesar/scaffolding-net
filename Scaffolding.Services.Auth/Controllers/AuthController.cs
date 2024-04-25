using Microsoft.AspNetCore.Mvc;
using Scaffolding.Core.DTOs;
using Scaffolding.Core.Interfaces;
using Scaffolding.Core.Services;
using System.Reflection;
using System;

namespace Scaffolding.Services.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO model)
        {
            try
            {
                var user = await _authService.RegisterAsync(model);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try {
                Console.WriteLine(login);
                var token = await _authService.LoginAsync(login);
                return Ok(new { token });
            }
            catch (Exception ex) 
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
