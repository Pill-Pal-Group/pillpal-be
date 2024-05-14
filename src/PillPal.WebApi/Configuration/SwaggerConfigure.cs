using Microsoft.OpenApi.Models;

namespace PillPal.WebApi.Configuration;

public static class SwaggerConfigure
{
    public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
    {
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        services.AddSwaggerGen(config =>
        {
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

            //var xmlFile = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
            //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            //config.IncludeXmlComments(xmlPath);

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
        });

        return services;
    }
}
