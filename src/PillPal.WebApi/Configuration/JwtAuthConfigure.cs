using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PillPal.WebApi.Configuration;

public static class JwtAuthConfigure
{
    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
    {
#pragma warning disable S3928, CA2208
        var iss = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer");

        var aud = configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience");

        var key = configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("Jwt:SecretKey");
#pragma warning restore S3928, CA2208

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = iss,
                    ValidAudience = aud,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                };
            });

        return services;
    }
}
