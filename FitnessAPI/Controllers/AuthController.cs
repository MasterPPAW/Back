using LibrarieModele;
using LibrarieModele.DTOs;

using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

using NivelService;
using NivelService.Abstraction;

namespace FitnessAPI.Controllers
{
    public class AuthController(IAuthService _authService) : Controller
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            // Validate input (e.g., check if email is unique, password meets requirements)
            var existingUser = await _authService.GetUserByEmail(userDTO.Email);
            if (existingUser != null)
                return BadRequest("Email is already in use.");

            var response = await _authService.Register(userDTO);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _authService.GetUserByEmail(request.Email);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            var isPasswordValid = _authService.VerifyPassword(user.Password, request.Password);
            if (!isPasswordValid)
                return Unauthorized("Invalid credentials.");

            var response = await _authService.Login(user);

            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}
