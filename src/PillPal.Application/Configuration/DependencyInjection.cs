using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Repositories;

namespace PillPal.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IActiveIngredientService, ActiveIngredientRepository>();
        services.AddScoped<IBrandService, BrandRepository>();
        services.AddScoped<ICategoryService, CategoryRepository>();
        services.AddScoped<ICustomerService, CustomerRepository>();
        //services.AddScoped<ICustomerPackageService, >();
        services.AddScoped<IDosageFormService, DosageFormRepository>();
        //services.AddScoped<IMedicationTakeService, >();
        services.AddScoped<IMedicineService, MedicineRepository>();
        services.AddScoped<INationService, NationRepository>();
        //services.AddScoped<IPackageCategoryService, >();
        //services.AddScoped<IPaymentService, >();
        services.AddScoped<IPharmaceuticalCompanyService, PharmaceuticalCompanyRepository>();
        services.AddScoped<IPharmacyStoreService, PharmacyStoreRepository>();
        //services.AddScoped<IPrescriptDetailService, >();
        //services.AddScoped<IPrescriptService, >();
        services.AddScoped<ISpecificationService, SpecificationRepository>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}