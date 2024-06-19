using PillPal.Application.Common.Interfaces.Auth;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Features.ActiveIngredients;
using PillPal.Application.Features.Auths;
using PillPal.Application.Features.Brands;
using PillPal.Application.Features.Categories;
using PillPal.Application.Features.Customers;
using PillPal.Application.Features.DosageForms;
using PillPal.Application.Features.Medicines;
using PillPal.Application.Features.Nations;
using PillPal.Application.Features.PharmaceuticalCompanies;
using PillPal.Application.Features.PharmacyStores;
using PillPal.Application.Features.Prescripts;
using PillPal.Application.Features.Specifications;

namespace PillPal.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthRepository>();

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
        services.AddScoped<IPrescriptService, PrescriptRepository>();
        services.AddScoped<ISpecificationService, SpecificationRepository>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}