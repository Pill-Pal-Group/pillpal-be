using PillPal.WebApi.Service;

namespace PillPal.WebApi;

public static class ServiceConfigure
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUser, UserAccessor>();

        services.AddHttpContextAccessor();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddProblemDetails();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddSwaggerDoc(configuration);

        services.AddJwtAuth();

        services.AddAuthorizationPolicy();

        services.AddCorsServices();

        services.AddRateLimiterServices(configuration);

        return services;
    }
}
