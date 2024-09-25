using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace PillPal.WebApi.Configuration;

public static class HealthChecksConfigure
{
    public static IServiceCollection AddHealthChecksServices(this IServiceCollection services)
    {
        services.AddHealthChecksUI(options =>
        {
            options.AddHealthCheckEndpoint("PillPal API", "/api/healthz");
        })
        .AddInMemoryStorage();

        return services;
    }

    public static void UseHealthChecksServices(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/api/healthz", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseHealthChecksUI(options =>
        {
            options.UIPath = "/healthchecks";
            options.PageTitle = "PillPal Health Checks UI";
            options.AddCustomStylesheet("wwwroot/css/healthchecks-ui.css");
        });
    }
}
