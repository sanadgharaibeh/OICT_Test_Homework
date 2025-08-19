namespace OICT_Test.Models
{
    internal class TokenResponse
    {
        public required string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}