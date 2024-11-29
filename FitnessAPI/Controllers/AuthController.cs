using LibrarieModele;
using LibrarieModele.DTOs;

using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

using NivelService;
using NivelService.Abstraction;

namespace FitnessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            // Validate input (e.g., check if email is unique, password meets requirements)
            var existingUser = await authService.GetUserByEmail(userDTO.Email);
            if (existingUser != null)
                return BadRequest("Email is already in use.");

            var response = await authService.Register(userDTO);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await authService.GetUserByEmail(request.Email);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            var isPasswordValid = authService.VerifyPassword(user.Password, request.Password);
            if (!isPasswordValid)
                return Unauthorized("Invalid credentials.");

            var response = await authService.Login(user);

            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}
