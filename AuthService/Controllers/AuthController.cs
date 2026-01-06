using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AuthService.DTOs;
using AuthService.Services;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly TokenService _tokenService = new();

    public AuthController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("User registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) return Unauthorized();

        var valid = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!valid) return Unauthorized();

        var token = _tokenService.CreateToken(user);
        return Ok(new { token });
    }
}