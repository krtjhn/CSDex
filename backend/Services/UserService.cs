// Import namespace: System.Threading.Tasks
using System.Threading.Tasks;
// Import namespace: System.Collections.Generic
using System.Collections.Generic;
// Import namespace: System.Linq
using System.Linq;
// Import namespace: Microsoft.EntityFrameworkCore
using Microsoft.EntityFrameworkCore;
// Import namespace: Oopdex.Api.Data
using Oopdex.Api.Data;
// Import namespace: Oopdex.Api.Models
using Oopdex.Api.Models;
// Empty line

// Define namespace: Oopdex.Api.Services
namespace Oopdex.Api.Services
// Start of block scope
{
    // Define interface: IUserService
    public interface IUserService
    // Start of block scope
    {
        // Execute line: Task<User> RegisterUserAsync(string username, string emai...
        Task<User> RegisterUserAsync(string username, string email, string password);
        // Execute line: Task<User> AuthenticateAsync(string username, string pass...
        Task<User> AuthenticateAsync(string username, string password);
        // Execute line: Task<User> AuthenticateByEmailAsync(string email, string ...
        Task<User> AuthenticateByEmailAsync(string email, string password);
        // Execute line: Task<User> GetUserByIdAsync(long id);
        Task<User> GetUserByIdAsync(long id);
        // Execute line: Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByUsernameAsync(string username);
        // Execute line: Task<User> UpdateUserProfileAsync(long id, string bio, st...
        Task<User> UpdateUserProfileAsync(long id, string bio, string trainerClass, string profilePictureUrl);
        // Execute line: Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetAllUsersAsync();
        // Execute line: Task<bool> DeleteUserAsync(long id);
        Task<bool> DeleteUserAsync(long id);
    // End of block scope
    }
// Empty line

    // Define class UserService inheriting/implementing IUserService
    public class UserService : IUserService
    // Start of block scope
    {
        // Execute line: private readonly OopdexDbContext _context;
        private readonly OopdexDbContext _context;
// Empty line

        // Constructor for class: UserService (Params: OopdexDbContext context)
        public UserService(OopdexDbContext context)
        // Start of block scope
        {
            // Execute line: _context = context;
            _context = context;
        // End of block scope
        }
// Empty line

        // Define method: RegisterUserAsync (Returns: Task<User>, Params: string username, string email, string password)
        public async Task<User> RegisterUserAsync(string username, string email, string password)
        // Start of block scope
        {
            // Control Flow: check condition 'if (await _context.Users.AnyAsync(u => u.Username == username))'
            if (await _context.Users.AnyAsync(u => u.Username == username))
                // Execute line: throw new System.Exception("Username is already taken");
                throw new System.Exception("Username is already taken");
// Empty line

            // Control Flow: check condition 'if (await _context.Users.AnyAsync(u => u.Email == email))'
            if (await _context.Users.AnyAsync(u => u.Email == email))
                // Execute line: throw new System.Exception("Email is already registered");
                throw new System.Exception("Email is already registered");
// Empty line

            // Execute line: var user = new User
            var user = new User
            // Start of block scope
            {
                // Execute line: Username = username,
                Username = username,
                // Execute line: Email = email,
                Email = email,
                // Execute line: Password = BCrypt.Net.BCrypt.HashPassword(password),
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                // Execute line: Role = "ROLE_USER"
                Role = "ROLE_USER"
            // Execute line: };
            };
// Empty line

            // Execute line: _context.Users.Add(user);
            _context.Users.Add(user);
            // Execute line: await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
// Empty line

            // Return statement: return user;
            return user;
        // End of block scope
        }
// Empty line

        // Define method: AuthenticateAsync (Returns: Task<User>, Params: string username, string password)
        public async Task<User> AuthenticateAsync(string username, string password)
        // Start of block scope
        {
            // Variable declaration and assignment: user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            // Control Flow: check condition 'if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))'
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                // Return statement: return null;
                return null;
// Empty line

            // Return statement: return user;
            return user;
        // End of block scope
        }
// Empty line

        // Define method: AuthenticateByEmailAsync (Returns: Task<User>, Params: string email, string password)
        public async Task<User> AuthenticateByEmailAsync(string email, string password)
        // Start of block scope
        {
            // Variable declaration and assignment: user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            // Control Flow: check condition 'if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))'
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                // Return statement: return null;
                return null;
// Empty line

            // Return statement: return user;
            return user;
        // End of block scope
        }
// Empty line

        // Define method: GetUserByIdAsync (Returns: Task<User>, Params: long id)
        public async Task<User> GetUserByIdAsync(long id)
        // Start of block scope
        {
            // Return statement: return await _context.Users
            return await _context.Users
                // Execute line: .Include(u => u.CaughtPokemons)
                .Include(u => u.CaughtPokemons)
                // Execute line: .FirstOrDefaultAsync(u => u.Id == id);
                .FirstOrDefaultAsync(u => u.Id == id);
        // End of block scope
        }
// Empty line

        // Define method: GetUserByUsernameAsync (Returns: Task<User>, Params: string username)
        public async Task<User> GetUserByUsernameAsync(string username)
        // Start of block scope
        {
            // Return statement: return await _context.Users
            return await _context.Users
                // Execute line: .Include(u => u.CaughtPokemons)
                .Include(u => u.CaughtPokemons)
                // Execute line: .FirstOrDefaultAsync(u => u.Username == username);
                .FirstOrDefaultAsync(u => u.Username == username);
        // End of block scope
        }
// Empty line

        // Define method: UpdateUserProfileAsync (Returns: Task<User>, Params: long id, string bio, string trainerClass, string profilePictureUrl)
        public async Task<User> UpdateUserProfileAsync(long id, string bio, string trainerClass, string profilePictureUrl)
        // Start of block scope
        {
            // Variable declaration and assignment: user = await _context.Users.FindAsync(id)
            var user = await _context.Users.FindAsync(id);
            // Control Flow: check condition 'if (user == null) return null;'
            if (user == null) return null;
// Empty line

            // Control Flow: check condition 'if (bio != null) user.Bio = bio;'
            if (bio != null) user.Bio = bio;
            // Control Flow: check condition 'if (trainerClass != null) user.TrainerClass = trainerClass;'
            if (trainerClass != null) user.TrainerClass = trainerClass;
            // Control Flow: check condition 'if (profilePictureUrl != null) user.ProfilePictureUrl = profilePictureUrl;'
            if (profilePictureUrl != null) user.ProfilePictureUrl = profilePictureUrl;
// Empty line

            // Execute line: await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            // Return statement: return user;
            return user;
        // End of block scope
        }
// Empty line

        // Define method: GetAllUsersAsync (Returns: Task<IEnumerable<User>>, Params: )
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        // Start of block scope
        {
            // Return statement: return await _context.Users.OrderBy(u => u.Id).ToListAsync();
            return await _context.Users.OrderBy(u => u.Id).ToListAsync();
        // End of block scope
        }
// Empty line

        // Define method: DeleteUserAsync (Returns: Task<bool>, Params: long id)
        public async Task<bool> DeleteUserAsync(long id)
        // Start of block scope
        {
            // Variable declaration and assignment: user = await _context.Users.FindAsync(id)
            var user = await _context.Users.FindAsync(id);
            // Control Flow: check condition 'if (user == null) return false;'
            if (user == null) return false;
// Empty line

            // Existing comment: Also remove all caught pokemons
            // Also remove all caught pokemons
            // Variable declaration and assignment: caught = await _context.CaughtPokemons.Where(c => c.UserId == id).ToListAsync()
            var caught = await _context.CaughtPokemons.Where(c => c.UserId == id).ToListAsync();
            // Execute line: _context.CaughtPokemons.RemoveRange(caught);
            _context.CaughtPokemons.RemoveRange(caught);
            // Execute line: _context.Users.Remove(user);
            _context.Users.Remove(user);
            // Execute line: await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            // Return statement: return true;
            return true;
        // End of block scope
        }
    // End of block scope
    }
// End of block scope
}
