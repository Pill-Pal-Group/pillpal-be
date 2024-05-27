using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.WebApi.Service;

namespace PillPal.WebApi.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddScoped<IUser, UserAccessor>();

        services.AddHttpContextAccessor();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddProblemDetails();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        //services.AddScoped<IJWTService<ApplicationUser>, JWTService<ApplicationUser>>();

        return services;
    }
}
