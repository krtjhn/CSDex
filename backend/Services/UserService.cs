using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oopdex.Api.Data;
using Oopdex.Api.Models;

namespace Oopdex.Api.Services
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(string username, string email, string password);
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> AuthenticateByEmailAsync(string email, string password);
        Task<User> GetUserByIdAsync(long id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(long id);
    }

    public class UserService : IUserService
    {
        private readonly OopdexDbContext _context;

        public UserService(OopdexDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUserAsync(string username, string email, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
                throw new System.Exception("Username is already taken");

            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
                throw new System.Exception("Email is already registered");

            var user = new User
            {
                Username = username,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Role = "ROLE_USER"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            return user;
        }

        public async Task<User> AuthenticateByEmailAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            return user;
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            return await _context.Users
                .Include(u => u.CaughtPokemons)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.CaughtPokemons)
                .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.OrderBy(u => u.Id).ToListAsync();
        }

        public async Task<bool> DeleteUserAsync(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            var caught = await _context.CaughtPokemons.Where(c => c.UserId == id).ToListAsync();
            _context.CaughtPokemons.RemoveRange(caught);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
