// Define namespace: Oopdex.Api.DTOs
namespace Oopdex.Api.DTOs
// Start of block scope
{
    // Define class AuthRequest
    public class AuthRequest
    // Start of block scope
    {
        // Property: Email (Type: string)
        public string Email { get; set; }
        // Property: Password (Type: string)
        public string Password { get; set; }
    // End of block scope
    }
// Empty line

    // Define class RegisterRequest
    public class RegisterRequest
    // Start of block scope
    {
        // Property: Username (Type: string)
        public string Username { get; set; }
        // Property: Email (Type: string)
        public string Email { get; set; }
        // Property: Password (Type: string)
        public string Password { get; set; }
    // End of block scope
    }
// Empty line

    // Define class AuthResponse
    public class AuthResponse
    // Start of block scope
    {
        // Property: Token (Type: string)
        public string Token { get; set; }
        // Property: Username (Type: string)
        public string Username { get; set; }
        // Property: Role (Type: string)
        public string Role { get; set; }
    // End of block scope
    }
// Empty line

    // Define class CatchPokemonRequest
    public class CatchPokemonRequest
    // Start of block scope
    {
        // Property: PokemonId (Type: int)
        public int PokemonId { get; set; }
        // Property: Nickname (Type: string?)
        public string? Nickname { get; set; }
    // End of block scope
    }
// Empty line

    // Define class UpdateCaughtPokemonRequest
    public class UpdateCaughtPokemonRequest
    // Start of block scope
    {
        // Property: Nickname (Type: string?)
        public string? Nickname { get; set; }
    // End of block scope
    }
// End of block scope
}
