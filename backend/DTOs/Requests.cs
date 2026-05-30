namespace Oopdex.Api.DTOs
{
    public class AuthRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }

    public class CatchPokemonRequest
    {
        public int PokemonId { get; set; }
        public string? Nickname { get; set; }
    }

    public class UpdateCaughtPokemonRequest
    {
        public string? Nickname { get; set; }
    }
}
