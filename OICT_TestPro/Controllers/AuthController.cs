
using Microsoft.AspNetCore.Mvc;
using OICT_Test.Helpers;
using OICT_Test.Models;

namespace OICT_Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly List<User> ApprovedUsers =
        [
            new() { Username = "Sanad", Password = "MyPassword" },
            new() { Username = "Tomas", Password = "MyPassword" }
        ];

        [HttpPost("token")]
        public IActionResult GetToken([FromBody] User user)
        {
            var authenticatedUser = ApprovedUsers
                .FirstOrDefault(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase) &&
                                     u.Password.Equals(user.Password, StringComparison.OrdinalIgnoreCase));

            if (authenticatedUser == null)
                return Unauthorized("Neplatné uživatelské jméno nebo heslo");

            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var expires = DateTime.UtcNow.AddHours(1);

            TokenStore.Tokens[token] = expires;

            return Ok(new TokenResponse
            {
                Token = token,
                Expires = expires
            });
        }
    }
}
