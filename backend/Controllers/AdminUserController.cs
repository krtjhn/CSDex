// Import namespace: System.Threading.Tasks
using System.Threading.Tasks;
// Import namespace: Microsoft.AspNetCore.Authorization
using Microsoft.AspNetCore.Authorization;
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
    // Execute line: [Route("api/admin/users")]
    [Route("api/admin/users")]
    // Attribute annotation: [Authorize(Roles = "ROLE_ADMIN")]
    [Authorize(Roles = "ROLE_ADMIN")]
    // Define class AdminUserController inheriting/implementing ControllerBase
    public class AdminUserController : ControllerBase
    // Start of block scope
    {
        // Execute line: private readonly IUserService _userService;
        private readonly IUserService _userService;
// Empty line

        // Constructor for class: AdminUserController (Params: IUserService userService)
        public AdminUserController(IUserService userService)
        // Start of block scope
        {
            // Execute line: _userService = userService;
            _userService = userService;
        // End of block scope
        }
// Empty line

        // Attribute annotation: [HttpGet]
        [HttpGet]
        // Define method: GetAllUsers (Returns: Task<IActionResult>, Params: )
        public async Task<IActionResult> GetAllUsers()
        // Start of block scope
        {
            // Variable declaration and assignment: users = await _userService.GetAllUsersAsync()
            var users = await _userService.GetAllUsersAsync();
            // Return statement: return Ok(users);
            return Ok(users);
        // End of block scope
        }
// Empty line

        // Execute line: [HttpDelete("{id}")]
        [HttpDelete("{id}")]
        // Define method: DeleteUser (Returns: Task<IActionResult>, Params: long id)
        public async Task<IActionResult> DeleteUser(long id)
        // Start of block scope
        {
            // Variable declaration and assignment: success = await _userService.DeleteUserAsync(id)
            var success = await _userService.DeleteUserAsync(id);
            // Control Flow: check condition 'if (!success) return NotFound(new { error = "User not found" });'
            if (!success) return NotFound(new { error = "User not found" });
            // Return statement: return NoContent();
            return NoContent();
        // End of block scope
        }
    // End of block scope
    }
// End of block scope
}
