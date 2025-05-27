using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SemikolonApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AuthService _authService;

        public AuthController(IConfiguration configuration, AuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest(new { message = "Brugernavn og kodeord kræves." });
            }

            var success = _authService.RegisterUser(user.UserName, user.Password);
            if (!success)
            {
                return BadRequest(new { message = "Brugernavn er allerede taget." });
            }

            return Ok(new { message = "Bruger registreret." });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        
        {
            var user = _authService.GetUserByUsername(request.UserName);
            if (user != null && _authService.VerifyPassword(request.Password, user.Password))
            {
                string token = CreateToken(user);
                return Ok(new { token });
            }

            return Unauthorized(new { message = "Ugyldigt login" });
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _authService.GetUsers();
            return Ok(users);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetValue<string>("Jwt:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
