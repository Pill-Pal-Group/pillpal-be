using Microsoft.AspNetCore.Identity;
using PillPal.Core.Configuration;
using PillPal.Core.Identity;
using PillPal.Infrastructure.Persistence;
using PillPal.Service.Configuration;
using PillPal.WebApi.Configuration;

namespace PillPal.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerDoc();

            builder.Services.AddMapper();

            builder.Services.AddServiceApplication();

            builder.Services.AddDbContext<ApplicationDbContext>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+()";
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddJwtAuth();

            var app = builder.Build();

            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
