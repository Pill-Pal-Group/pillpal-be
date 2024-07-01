using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using PillPal.Application.Common.Interfaces.Auth;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Infrastructure.Auth;
using PillPal.Infrastructure.Identity;
using PillPal.Infrastructure.Persistence;
using PillPal.Infrastructure.Persistence.Interceptors;

namespace PillPal.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, UniqueCodeInterceptor>();

        services.AddDbContext<ApplicationDbContext>((service, options) =>
        {
            options.AddInterceptors(service.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(configuration.GetConnectionString("PILLPAL_DB"));
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);

        services.AddIdentityCore<ApplicationUser>(op =>
        {
            op.Password.RequireDigit = true;
            op.Password.RequireLowercase = true;
            op.Password.RequireNonAlphanumeric = true;
            op.Password.RequireUppercase = true;
            op.Password.RequiredLength = 6;

            op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            op.Lockout.MaxFailedAccessAttempts = 5;
            op.Lockout.AllowedForNewUsers = true;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddTransient<IIdentityService, IdentityService>();

        services.Configure<JwtSettings>(options =>
        {
            options.SecretKey = configuration["Jwt:SecretKey"];
            options.Issuer = configuration["Jwt:Issuer"];
            options.Audience = configuration["Jwt:Audience"];
            options.Expires = double.Parse(configuration["Jwt:Expires"]!);
        });

        services.AddTransient<IJwtService, JwtService>();

        services.Configure<FirebaseSettings>(options =>
        {
            options.ProjectId = configuration["Firebase:ProjectId"];
            options.ServiceKey = configuration["Firebase:ServiceKey"];
        });

        services.AddSingleton<IFirebaseService, FirebaseService>();

        services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(configuration.GetConnectionString("HANGFIRE_DB"));
        });

        services.AddHangfireServer();

        return services;
    }

    public static void UseInfrastructureServices(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DarkModeEnabled = false,
            DashboardTitle = "PillPal Hangfire Dashboard",
            Authorization = new[]
            {
                new HangfireCustomBasicAuthenticationFilter
                {
                    User = configuration["Hangfire:User"],
                    Pass = configuration["Hangfire:Pass"]
                }
            }
        });

        RecurringJob.AddOrUpdate<ICustomerPackageService>(
            "CheckForExpiredPackages",
            service => service.CheckForExpiredPackagesAsync(),
            Cron.Daily,
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            }
        );
    }
}
