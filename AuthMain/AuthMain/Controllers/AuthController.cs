using AuthMain.Model;
using AuthMain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthMain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtTokenGenerator _jwt;

        public AuthController(UserService userService, JwtTokenGenerator jwt)
        {
            _userService = userService;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var userExists = await _userService.GetByEmailAsync(dto.Email);
            if (userExists != null) return BadRequest("User already exists");

            var user = new User
            {
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role ?? "Student",
            };

            await _userService.CreateAsync(user);
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userService.GetByEmailAsync(dto.Email);
            if (user == null || user.Password != dto.Password)
                return Unauthorized("Invalid credentials");

            var tokens = _jwt.GenerateTokens(user);

            user.RefreshToken = tokens.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userService.UpdateAsync(user.Id!, user);

            return Ok(tokens);
        }
    }
}
