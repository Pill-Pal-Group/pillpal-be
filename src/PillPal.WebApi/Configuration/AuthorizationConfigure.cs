using PillPal.Core.Constant;

namespace PillPal.WebApi.Configuration;

public static class AuthorizationConfigure
{
    public static IServiceCollection AddAuthorizationPolicy(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(Policy.Administrative, policy => policy.RequireRole(Role.Admin, Role.Manager))
            .AddPolicy(Policy.Admin, policy => policy.RequireRole(Role.Admin))
            .AddPolicy(Policy.Manager, policy => policy.RequireRole(Role.Manager))
            .AddPolicy(Policy.Customer, policy => policy.RequireRole(Role.Customer));

        return services;
    }
}
