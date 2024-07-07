using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using PillPal.Application.Features.Auths;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace PillPal.WebApi.Configuration;

public static class SwaggerConfigure
{
    public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
    {
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        services.AddSwaggerGen(config =>
        {
            config.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

            config.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PillPal.WebApi",
                Description = $"PillPal WebApi version 1 ({environment})",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "PillPal Group",
                    Email = "pillpalincorporation@gmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            var controllerXmlFile = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
            var controllerXmlPath = Path.Combine(AppContext.BaseDirectory, controllerXmlFile);

            config.IncludeXmlComments(controllerXmlPath);

            var dtoXmlFile = Assembly.GetAssembly(typeof(AuthRepository))!.GetName().Name + ".xml";
            var dtoXmlPath = Path.Combine(AppContext.BaseDirectory, dtoXmlFile);

            config.IncludeXmlComments(dtoXmlPath);

            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            if (environment == "Development")
            {
                config.AddServer(new OpenApiServer
                {
                    Url = "http://localhost:5209",
                    Description = "Local server - http"
                });

                config.AddServer(new OpenApiServer
                {
                    Url = "https://localhost:7299",
                    Description = "Local server - https"
                });
            }

            config.AddServer(new OpenApiServer
            {
                Url = "https://pp-devtest2.azurewebsites.net",
                Description = "Staging server"
            });

        })
        .AddFluentValidationRulesToSwagger();

        return services;
    }
}
