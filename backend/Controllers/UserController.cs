// Import namespace: System
using System;
// Import namespace: System.IO
using System.IO;
// Import namespace: System.Security.Claims
using System.Security.Claims;
// Import namespace: System.Threading.Tasks
using System.Threading.Tasks;
// Import namespace: Microsoft.AspNetCore.Authorization
using Microsoft.AspNetCore.Authorization;
// Import namespace: Microsoft.AspNetCore.Http
using Microsoft.AspNetCore.Http;
// Import namespace: Microsoft.AspNetCore.Mvc
using Microsoft.AspNetCore.Mvc;
// Import namespace: Oopdex.Api.Services
using Oopdex.Api.Services;
// Empty line

// Define namespace: Oopdex.Api.Controllers
namespace Oopdex.Api.Controllers
// Start of block scope
{
    // Attribute annotation: [ApiController]
    [ApiController]
    // Execute line: [Route("api/user")]
    [Route("api/user")]
    // Attribute annotation: [Authorize]
    [Authorize]
    // Define class UserController inheriting/implementing ControllerBase
    public class UserController : ControllerBase
    // Start of block scope
    {
        // Execute line: private readonly IUserService _userService;
        private readonly IUserService _userService;
// Empty line

        // Constructor for class: UserController (Params: IUserService userService)
        public UserController(IUserService userService)
        // Start of block scope
        {
            // Execute line: _userService = userService;
            _userService = userService;
        // End of block scope
        }
// Empty line

        // Attribute annotation: [HttpGet("me")]
        [HttpGet("me")]
        // Define method: GetCurrentUser (Returns: Task<IActionResult>, Params: )
        public async Task<IActionResult> GetCurrentUser()
        // Start of block scope
        {
            // Variable declaration and assignment: userIdStr = User.FindFirstValue("userId")
            var userIdStr = User.FindFirstValue("userId");
            // Control Flow: check condition 'if (!long.TryParse(userIdStr, out var userId))'
            if (!long.TryParse(userIdStr, out var userId))
                // Return statement: return Unauthorized();
                return Unauthorized();
// Empty line

            // Variable declaration and assignment: user = await _userService.GetUserByIdAsync(userId)
            var user = await _userService.GetUserByIdAsync(userId);
            // Control Flow: check condition 'if (user == null) return NotFound();'
            if (user == null) return NotFound();
// Empty line

            // Return statement: return Ok(user);
            return Ok(user);
        // End of block scope
        }
// Empty line

        // Attribute annotation: [HttpGet("profile")]
        [HttpGet("profile")]
        // Define method: GetProfile (Returns: Task<IActionResult>, Params: )
        public async Task<IActionResult> GetProfile()
        // Start of block scope
        {
            // Variable declaration and assignment: userIdStr = User.FindFirstValue("userId")
            var userIdStr = User.FindFirstValue("userId");
            // Control Flow: check condition 'if (!long.TryParse(userIdStr, out var userId))'
            if (!long.TryParse(userIdStr, out var userId))
                // Return statement: return Unauthorized();
                return Unauthorized();
// Empty line

            // Variable declaration and assignment: user = await _userService.GetUserByIdAsync(userId)
            var user = await _userService.GetUserByIdAsync(userId);
            // Control Flow: check condition 'if (user == null) return NotFound();'
            if (user == null) return NotFound();
// Empty line

            // Return statement: return Ok(user);
            return Ok(user);
        // End of block scope
        }
// Empty line

        // Execute line: [HttpPost("profile/upload-picture")]
        [HttpPost("profile/upload-picture")]
        // Define method: UploadProfilePicture (Returns: Task<IActionResult>, Params: IFormFile file)
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        // Start of block scope
        {
            // Control Flow: check condition 'if (file == null || file.Length == 0)'
            if (file == null || file.Length == 0)
                // Return statement: return BadRequest(new { error = "Please select a file to upload." });
                return BadRequest(new { error = "Please select a file to upload." });
// Empty line

            // Variable declaration and assignment: userIdStr = User.FindFirstValue("userId")
            var userIdStr = User.FindFirstValue("userId");
            // Control Flow: check condition 'if (!long.TryParse(userIdStr, out var userId))'
            if (!long.TryParse(userIdStr, out var userId))
                // Return statement: return Unauthorized();
                return Unauthorized();
// Empty line

            // Variable declaration and assignment: uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "profile-pictures")
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "profile-pictures");
            // Control Flow: check condition 'if (!Directory.Exists(uploadsPath))'
            if (!Directory.Exists(uploadsPath))
                // Execute line: Directory.CreateDirectory(uploadsPath);
                Directory.CreateDirectory(uploadsPath);
// Empty line

            // Variable declaration and assignment: fileName = $"{userId}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}"
            var fileName = $"{userId}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            // Variable declaration and assignment: filePath = Path.Combine(uploadsPath, fileName)
            var filePath = Path.Combine(uploadsPath, fileName);
// Empty line

            // Execute line: using (var stream = new FileStream(filePath, FileMode.Cre...
            using (var stream = new FileStream(filePath, FileMode.Create))
            // Start of block scope
            {
                // Execute line: await file.CopyToAsync(stream);
                await file.CopyToAsync(stream);
            // End of block scope
            }
// Empty line

            // Variable declaration and assignment: fileUrl = $"/uploads/profile-pictures/{fileName}"
            var fileUrl = $"/uploads/profile-pictures/{fileName}";
            // Execute line: await _userService.UpdateUserProfileAsync(userId, null, n...
            await _userService.UpdateUserProfileAsync(userId, null, null, fileUrl);
// Empty line

            // Return statement: return Ok(new { url = fileUrl });
            return Ok(new { url = fileUrl });
        // End of block scope
        }
    // End of block scope
    }
// End of block scope
}
