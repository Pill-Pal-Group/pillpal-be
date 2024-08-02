using Hangfire;
using HangfireBasicAuthenticationFilter;
using PillPal.Infrastructure.Auth;
using PillPal.Infrastructure.Cache;
using PillPal.Infrastructure.File;
using PillPal.Infrastructure.Identity;
using PillPal.Infrastructure.Persistence;
using PillPal.Infrastructure.Persistence.Interceptors;
using PillPal.Infrastructure.PaymentService.VnPay;
using PillPal.Infrastructure.PaymentService.ZaloPay;
using PillPal.Application.Common.Interfaces.Payment;

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

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddTransient<IIdentityService, IdentityService>();

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddTransient<IJwtService, JwtService>();

        services.Configure<FirebaseSettings>(configuration.GetSection("FirebaseSettings"));

        services.AddSingleton<IFirebaseService, FirebaseService>();

        services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(configuration.GetConnectionString("HANGFIRE_DB"));
        });

        services.AddHangfireServer();

        services.AddScoped<IFileReader, FileReader>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("REDIS");
        });

        services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));

        services.AddScoped<ICacheService, CacheService>();

        services.Configure<VnPayConfiguration>(configuration.GetSection("VnPay"));

        services.Configure<ZaloPayConfiguration>(configuration.GetSection("ZaloPay"));

        services.AddScoped<IVnPayService, VnPayService>();
        services.AddScoped<IZaloPayService, ZaloPayService>();

        return services;
    }

    public static void UseInfrastructureServices(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DarkModeEnabled = false,
            DashboardTitle = "PillPal Hangfire Dashboard",
            DisplayStorageConnectionString = false,
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

        RecurringJob.AddOrUpdate<ICustomerPackageService>(
            "MessageToRenewPackage",
            service => service.CheckForRenewPackage(),
            Cron.Daily,
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            }
        );
    }
}
