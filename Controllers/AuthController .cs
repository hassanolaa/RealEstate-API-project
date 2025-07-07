using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using realstate.Services.DTOs;
using realstate.Services.Repositories.Interfaces;

namespace realstate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly Services.Repositories.Interfaces.ITokenService _tokenService;

        public AuthController(IUserService userService, Services.Repositories.Interfaces.ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {
            var user = await _userService.CreateAsync(dto);
            return CreatedAtAction(nameof(Login), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var valid = await _userService.ValidateCredentialsAsync(dto.Email, dto.Password);
            if (!valid) return Unauthorized("Invalid credentials");

            var user = await _userService.GetByEmailAsync(dto.Email);
            if (user == null) return Unauthorized();

            var token = _tokenService.GenerateToken(user);
            await _userService.UpdateLastLoginAsync(user.Id);

            return Ok(new { Token = token });
        }
    }
}
    