using PillPal.Application.Common.Interfaces.Auth;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Infrastructure.Auth;
using PillPal.Infrastructure.Identity;
using PillPal.Infrastructure.Persistence;
using PillPal.Infrastructure.Persistence.Interceptors;

namespace PillPal.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PILLPAL_DB");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, UniqueCodeInterceptor>();

        services.AddDbContext<ApplicationDbContext>((service, options) =>
        {
            options.AddInterceptors(service.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);

        services.AddIdentityCore<ApplicationUser>(op =>
        {
            op.Password.RequireDigit = false;
            op.Password.RequireLowercase = false;
            op.Password.RequireNonAlphanumeric = false;
            op.Password.RequireUppercase = false;
            op.Password.RequiredLength = 6;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddTransient<IIdentityService, IdentityService>();

        services.AddTransient<IJwtService, JwtService>();

        return services;
    }
}
