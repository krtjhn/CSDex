using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oopdex.Api.Services;

namespace Oopdex.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdStr = User.FindFirstValue("userId");
            if (!long.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdStr = User.FindFirstValue("userId");
            if (!long.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost("profile/upload-picture")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { error = "Please select a file to upload." });

            var userIdStr = User.FindFirstValue("userId");
            if (!long.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "profile-pictures");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = $"{userId}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = $"/uploads/profile-pictures/{fileName}";
            await _userService.UpdateUserProfileAsync(userId, null, null, fileUrl);

            return Ok(new { url = fileUrl });
        }
    }
}
