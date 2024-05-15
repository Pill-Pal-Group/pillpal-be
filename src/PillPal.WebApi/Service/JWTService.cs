using Microsoft.IdentityModel.Tokens;
using PillPal.Core.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PillPal.WebApi.Service;

public interface IJWTService<T> where T : ApplicationUser
{
    string CreateToken(T user);
}

public class JWTService<T> : IJWTService<T> where T : ApplicationUser
{
    private readonly IConfiguration _configuration;

    public JWTService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(T user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_configuration["JwtSecret"]);

        var firebaseId = _configuration["FIREBASE_ID"] ?? "not-pillpal";

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                //new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            IssuedAt = DateTime.UtcNow,
            Issuer = $"https://securetoken.google.com/{firebaseId}",
            Audience = firebaseId,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
