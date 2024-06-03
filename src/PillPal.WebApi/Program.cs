using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PillPal.Application.Configuration;
using PillPal.Core.Constant;
using PillPal.Infrastructure.Configuration;
using PillPal.WebApi.Configuration;

namespace PillPal.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(
                    new KebabCaseParameterTransformer()));
            });

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebServices();

            builder.Services.AddJwtAuth(builder.Configuration);

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Role.Admin, policy => policy.RequireRole(Role.Admin));
                options.AddPolicy(Role.Manager, policy => policy.RequireRole(Role.Manager));
                options.AddPolicy(Role.Customer, policy => policy.RequireRole(Role.Customer));
            });

            var app = builder.Build();

            app.UseExceptionHandler();

            if (app.Environment.IsDevelopment()
                || app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
