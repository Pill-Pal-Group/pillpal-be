using Microsoft.Extensions.DependencyInjection;
using PillPal.Infrastructure.Repository;
using PillPal.Service.Applications.DrugInformations;
using PillPal.Service.Applications.Drugs;
using PillPal.Service.Applications.Ingredients;
using PillPal.Service.Identities;

namespace PillPal.Service.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceApplication(this IServiceCollection services)
    {
        services.AddScoped<UnitOfWork>();

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IDrugService, DrugService>();
        services.AddScoped<IDrugInformationService, DrugInformationService>();
        services.AddScoped<IIngredientService, IngredientService>();

        return services;
    }
}
