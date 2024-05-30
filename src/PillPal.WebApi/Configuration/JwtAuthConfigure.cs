using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PillPal.WebApi.Configuration;

public static class JwtAuthConfigure
{
    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var iss = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer");

        var aud = configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience");

        var key = configuration["Firebase:ProjectId"] ?? throw new ArgumentNullException("Firebase:ProjectId");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                //option.Authority = $"https://securetoken.google.com/{firebaseId}";
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = iss,
                    ValidAudience = aud,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                };
            });


        return services;
    }
}
