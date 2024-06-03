using Microsoft.IdentityModel.Tokens;
using PillPal.Application.Common.Interfaces.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PillPal.Infrastructure.Auth
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenHandler _tokenHandler = new();

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

#pragma warning disable S3928, CA2208
        private string SecretKey => _configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("Jwt:SecretKey");

        private string Issuer => _configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer");

        private string Audience => _configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience");

        private double Expires => double.Parse(_configuration["Jwt:Expires"] ?? throw new ArgumentNullException("Jwt:Expires"));

        private string FirebaseProjectId => _configuration["Firebase:ProjectId"] ?? throw new ArgumentNullException("Firebase:ProjectId");

        private string FirebasePublicKey => _configuration["Firebase:PublicKey"] ?? throw new ArgumentNullException("Firebase:PublicKey");
#pragma warning restore S3928, CA2208

        public (string accessToken, int expired) GenerateJwtToken(ApplicationUser user, string role)
        {
            var signingCredential = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey)), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Role, role)
                ]),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(Expires),
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = signingCredential
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);

            return (_tokenHandler.WriteToken(token), (int)Expires);
        }

        public string GenerateRefreshToken(string token)
        {
            return "abc";
        }

        public string GetEmailPrincipal(string token)
        {
            var signingKey = new X509SecurityKey(new X509Certificate2(Convert.FromBase64String(FirebasePublicKey)));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,

                ValidIssuer = $"https://securetoken.google.com/{FirebaseProjectId}",
                IssuerSigningKey = signingKey,
                ClockSkew = TimeSpan.Zero
            };

            var principal = _tokenHandler.ValidateToken(token, validationParameters, out _);

            return principal.FindFirst(ClaimTypes.Email)?.Value!;
        }
    }
}
