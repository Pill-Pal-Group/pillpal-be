using PillPal.Core.Constant;

namespace PillPal.WebApi.Configuration;

public static class AuthorizationConfigure
{
    public static IServiceCollection AddAuthorizationPolicy(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(Role.Admin, policy => policy.RequireRole(Role.Admin))
            .AddPolicy(Role.Manager, policy => policy.RequireRole(Role.Manager))
            .AddPolicy(Role.Customer, policy => policy.RequireRole(Role.Customer));

        return services;
    }
}
