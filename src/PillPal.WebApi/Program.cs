using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PillPal.Application.Configuration;
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

            builder.Services.AddSwaggerDoc();

            builder.Services.AddCorsServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebServices();

            //builder.Services.AddJwtAuth();

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
