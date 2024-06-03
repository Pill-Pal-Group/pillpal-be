﻿using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Data;
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

        services.AddSwaggerDoc();

        services.AddJwtAuth(configuration);

        services.AddAuthorizationPolicy();

        services.AddCorsServices();

        return services;
    }
}
