// Import namespace: System
using System;
// Import namespace: System.IdentityModel.Tokens.Jwt
using System.IdentityModel.Tokens.Jwt;
// Import namespace: System.Security.Claims
using System.Security.Claims;
// Import namespace: System.Text
using System.Text;
// Import namespace: Microsoft.Extensions.Configuration
using Microsoft.Extensions.Configuration;
// Import namespace: Microsoft.IdentityModel.Tokens
using Microsoft.IdentityModel.Tokens;
// Import namespace: Oopdex.Api.Models
using Oopdex.Api.Models;

// Define namespace: Oopdex.Api.Services
namespace Oopdex.Api.Services
// Start of block scope
{
    // Define interface: IJwtTokenService
    public interface IJwtTokenService
    // Start of block scope
    {
        // Execute line: string GenerateToken(User user);
        string GenerateToken(User user);
    // End of block scope
    }

    // Define class JwtTokenService inheriting/implementing IJwtTokenService
    public class JwtTokenService : IJwtTokenService
    // Start of block scope
    {
        // Execute line: private readonly IConfiguration _configuration;
        private readonly IConfiguration _configuration;

        // Constructor for class: JwtTokenService (Params: IConfiguration configuration)
        public JwtTokenService(IConfiguration configuration)
        // Start of block scope
        {
            // Execute line: _configuration = configuration;
            _configuration = configuration;
        // End of block scope
        }

        // Define method: GenerateToken (Returns: string, Params: User user)
        public string GenerateToken(User user)
        // Start of block scope
        {
            // Variable declaration and assignment: secretKey = _configuration["Jwt:Secret"]
            var secretKey = _configuration["Jwt:Secret"];
            // Variable declaration and assignment: expirationInMinutes = _configuration.GetValue<int>("Jwt:ExpirationInMinutes")
            var expirationInMinutes = _configuration.GetValue<int>("Jwt:ExpirationInMinutes");

            // Variable declaration and assignment: securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            // Variable declaration and assignment: credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Execute line: var claims = new[]
            var claims = new[]
            // Start of block scope
            {
                // Execute line: new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                // Execute line: new Claim("userId", user.Id.ToString()),
                new Claim("userId", user.Id.ToString()),
                // Execute line: new Claim("role", user.Role ?? "ROLE_USER"),
                new Claim("role", user.Role ?? "ROLE_USER"),
                // Execute line: new Claim("username", user.Username)
                new Claim("username", user.Username)
            // Execute line: };
            };

            // Execute line: var token = new JwtSecurityToken(
            var token = new JwtSecurityToken(
                // Execute line: claims: claims,
                claims: claims,
                // Execute line: expires: DateTime.Now.AddMinutes(expirationInMinutes),
                expires: DateTime.Now.AddMinutes(expirationInMinutes),
                // Execute line: signingCredentials: credentials);
                signingCredentials: credentials);

            // Return statement: return new JwtSecurityTokenHandler().WriteToken(token);
            return new JwtSecurityTokenHandler().WriteToken(token);
        // End of block scope
        }
    // End of block scope
    }
// End of block scope
}
