using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oopdex.Api.DTOs;
using Oopdex.Api.Services;

namespace Oopdex.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IUserService userService, IJwtTokenService jwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(request.Username, request.Email, request.Password);
                return Ok(new { message = "User registered successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            var user = await _userService.AuthenticateByEmailAsync(request.Email, request.Password);
            if (user == null)
            {
                return Unauthorized(new { error = "Invalid credentials" });
            }

            var token = _jwtTokenService.GenerateToken(user);
            return Ok(new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Role = user.Role ?? "ROLE_USER"
            });
        }
    }
}
