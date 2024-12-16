using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using mangaEnjoy.Entities;
using mangaEnjoy.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace mangaEnjoy.Helpers;

public class JwtProvider : IJwtProvider
{

    public string GenerateToken(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey("supermegasigmaultrapupersecretkey"u8.ToArray()),
                SecurityAlgorithms.HmacSha256),
            expires: DateTime.UtcNow.AddHours(12)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}