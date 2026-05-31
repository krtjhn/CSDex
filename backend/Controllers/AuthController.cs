// Import namespace: System
using System;
// Import namespace: System.Threading.Tasks
using System.Threading.Tasks;
// Import namespace: Microsoft.AspNetCore.Mvc
using Microsoft.AspNetCore.Mvc;
// Import namespace: Oopdex.Api.DTOs
using Oopdex.Api.DTOs;
// Import namespace: Oopdex.Api.Services
using Oopdex.Api.Services;

// Define namespace: Oopdex.Api.Controllers
namespace Oopdex.Api.Controllers
// Start of block scope
{
    // Attribute annotation: [ApiController]
    [ApiController]
    // Execute line: [Route("api/auth")]
    [Route("api/auth")]
    // Define class AuthController inheriting/implementing ControllerBase
    public class AuthController : ControllerBase
    // Start of block scope
    {
        // Execute line: private readonly IUserService _userService;
        private readonly IUserService _userService;
        // Execute line: private readonly IJwtTokenService _jwtTokenService;
        private readonly IJwtTokenService _jwtTokenService;

        // Constructor for class: AuthController (Params: IUserService userService, IJwtTokenService jwtTokenService)
        public AuthController(IUserService userService, IJwtTokenService jwtTokenService)
        // Start of block scope
        {
            // Execute line: _userService = userService;
            _userService = userService;
            // Execute line: _jwtTokenService = jwtTokenService;
            _jwtTokenService = jwtTokenService;
        // End of block scope
        }

        // Attribute annotation: [HttpPost("register")]
        [HttpPost("register")]
        // Define method: Register (Returns: Task<IActionResult>, Params: [FromBody] RegisterRequest request)
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        // Start of block scope
        {
            // Execute line: try
            try
            // Start of block scope
            {
                // Variable declaration and assignment: user = await _userService.RegisterUserAsync(request.Username, request.Email, request.Password)
                var user = await _userService.RegisterUserAsync(request.Username, request.Email, request.Password);
                // Return statement: return Ok(new { message = "User registered successfully!" });
                return Ok(new { message = "User registered successfully!" });
            // End of block scope
            }
            // Execute line: catch (Exception ex)
            catch (Exception ex)
            // Start of block scope
            {
                // Return statement: return BadRequest(new { error = ex.Message });
                return BadRequest(new { error = ex.Message });
            // End of block scope
            }
        // End of block scope
        }

        // Attribute annotation: [HttpPost("login")]
        [HttpPost("login")]
        // Define method: Login (Returns: Task<IActionResult>, Params: [FromBody] AuthRequest request)
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        // Start of block scope
        {
            // Variable declaration and assignment: user = await _userService.AuthenticateByEmailAsync(request.Email, request.Password)
            var user = await _userService.AuthenticateByEmailAsync(request.Email, request.Password);
            // Control Flow: check condition 'if (user == null)'
            if (user == null)
            // Start of block scope
            {
                // Return statement: return Unauthorized(new { error = "Invalid credentials" });
                return Unauthorized(new { error = "Invalid credentials" });
            // End of block scope
            }

            // Variable declaration and assignment: token = _jwtTokenService.GenerateToken(user)
            var token = _jwtTokenService.GenerateToken(user);
            // Return statement: return Ok(new AuthResponse
            return Ok(new AuthResponse
            // Start of block scope
            {
                // Execute line: Token = token,
                Token = token,
                // Execute line: Username = user.Username,
                Username = user.Username,
                // Execute line: Role = user.Role ?? "ROLE_USER"
                Role = user.Role ?? "ROLE_USER"
            // Execute line: });
            });
        // End of block scope
        }
    // End of block scope
    }
// End of block scope
}
