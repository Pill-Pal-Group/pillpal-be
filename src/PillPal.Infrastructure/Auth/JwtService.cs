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

        private string SecretKey => _configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("Jwt:SecretKey");
        private string Issuer => _configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer");
        private string Audience => _configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience");
        private double Expires => double.Parse(_configuration["Jwt:Expires"] ?? throw new ArgumentNullException("Jwt:Expires"));

        private string FirebaseProjectId => _configuration["Firebase:ProjectId"] ?? throw new ArgumentNullException("Firebase:ProjectId");

        public string GenerateJwtToken(ApplicationUser user, string role)
        {
            var signingCredential = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey)), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, role)
                ]),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(Expires),
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = signingCredential
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);

            return _tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            //var firebasePublicKey = "MIIDHTCCAgWgAwIBAgIJALRetqNfe++KMA0GCSqGSIb3DQEBBQUAMDExLzAtBgNVBAMMJnNlY3VyZXRva2VuLnN5c3RlbS5nc2VydmljZWFjY291bnQuY29tMB4XDTI0MDUyNDA3MzIyNFoXDTI0MDYwOTE5NDcyNFowMTEvMC0GA1UEAwwmc2VjdXJldG9rZW4uc3lzdGVtLmdzZXJ2aWNlYWNjb3VudC5jb20wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQC9JdDLpKYwAzg6uspB5Y5l4wGiR366Am1PfLxnzbxkFqqQuktB8/v0ie8yRO/OIIdgoDKbzSSNTGX+h972V8QPgDSX5ljOdBaf0Vc+hu9chQptl6UEWoDaV+iFY3i8Rt7DHTnrFwwY7L0/4lbCK2oQDCCz0HIJfNNq2o8MF0em/tD23PEz6oAMULbA5Cd+5v9xpFcVIyMF8Az5twwUezwdufFZVJKI9gZu+OeVx0hX9znTASL0MFiDSt2FpRwwUM8bXQQBSumbt5sRNaqo9pxety99zXq5zX80Z7PA2VfIFSgq3f7dkrqdR71arYt88x+UEwHCdIbj4KUFHpdrB3h/AgMBAAGjODA2MAwGA1UdEwEB/wQCMAAwDgYDVR0PAQH/BAQDAgeAMBYGA1UdJQEB/wQMMAoGCCsGAQUFBwMCMA0GCSqGSIb3DQEBBQUAA4IBAQAXEMjRHu2Ad/qiAOXQMKdMKFye+oi8Wn/IL23NKRTkVk1MsAzrwzXQJH9FwXEJ0wGrkLCvDMVe0RiHvdNDq1LMziHG/tridQMe/RtT5w3qFlKbsiGANhqm5Z6Mv0lXLAE9efsGV1SSTBgf+9KsFdgv3VplyvelGNOTFdnu/4qT1yiRihlZTUSCI8YYhN/v6Y2GFS8lzRLWEJp8XKXh/ZiMQabjh+LTAzhxErSynN2wze6nokYRnyLqijUo+HacCMBqoEsI48CUByayJUYB27d0WA3fkWLmmMy24JYIcESDb6Wv9TENI6L42J51H8qrW99iSCUpRGlwBANX5z350IXf";

            var firebasePublicKey = _configuration["Firebase:PublicKey"] ?? throw new ArgumentNullException("Firebase:PublicKey");

            var signingKey = new X509SecurityKey(new X509Certificate2(Convert.FromBase64String(firebasePublicKey)));

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

            return principal;
        }

        public string GetUserEmailFromToken(string token)
        {
            var principal = GetPrincipalFromToken(token);

            return principal.FindFirst(ClaimTypes.Email)?.Value;
        }

        public bool ValidateFirebaseToken(string token)
        {
            // var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

            //var firebase = _configuration["Firebase:ProjectId"] ?? throw new ArgumentNullException("Firebase:ProjectId");

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                // ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,

                //ValidIssuer = $"https://securetoken.google.com/not-pillpall",
                // ValidAudience = "pillpal-1",
                // IssuerSigningKey = signingKey,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                _tokenHandler.ValidateToken(token, validationParameters, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
