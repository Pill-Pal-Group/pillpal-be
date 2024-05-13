using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace PillPal.WebApi.Configuration;

public static class JwtAuthConfigure
{
    public static IServiceCollection AddJwtAuth(this IServiceCollection services)
    {
        var firebaseId = Environment.GetEnvironmentVariable("FIREBASE_ID") ?? "not-pillpal";

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.Authority = $"https://securetoken.google.com/{firebaseId}";
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"https://securetoken.google.com/{firebaseId}",
                    ValidateAudience = true,
                    ValidAudience = firebaseId,
                    ValidateLifetime = true
                };
            });

        return services;
    }
}
