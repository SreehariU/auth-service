using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Services;

public class TokenService
{
    public string CreateToken(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Email!)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("SUPER_SECRET_KEY_12345_CHANGE_THIS"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}