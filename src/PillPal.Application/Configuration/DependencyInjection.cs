﻿using PillPal.Application.Features.Accounts;
using PillPal.Application.Features.ActiveIngredients;
using PillPal.Application.Features.Auths;
using PillPal.Application.Features.Brands;
using PillPal.Application.Features.Categories;
using PillPal.Application.Features.CustomerPackages;
using PillPal.Application.Features.Customers;
using PillPal.Application.Features.DosageForms;
using PillPal.Application.Features.MedicationTakes;
using PillPal.Application.Features.Medicines;
using PillPal.Application.Features.Nations;
using PillPal.Application.Features.PackageCategories;
using PillPal.Application.Features.Payments;
using PillPal.Application.Features.PharmaceuticalCompanies;
using PillPal.Application.Features.Prescripts;
using PillPal.Application.Features.Specifications;
using PillPal.Application.Features.Statistics;
using PillPal.Application.Features.TermsOfServices;

namespace PillPal.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthRepository>();
        services.AddScoped<IAccountService, AccountRepository>();

        services.AddScoped<IActiveIngredientService, ActiveIngredientRepository>();
        services.AddScoped<IBrandService, BrandRepository>();
        services.AddScoped<ICategoryService, CategoryRepository>();
        services.AddScoped<ICustomerService, CustomerRepository>();
        services.AddScoped<ICustomerPackageService, CustomerPackageRepository>();
        services.AddScoped<IDosageFormService, DosageFormRepository>();
        services.AddScoped<IMedicationTakeService, MedicationTakeRepository>();
        services.AddScoped<IMedicineService, MedicineRepository>();
        services.AddScoped<INationService, NationRepository>();
        services.AddScoped<IPackageCategoryService, PackageCategoryRepository>();
        services.AddScoped<IPharmaceuticalCompanyService, PharmaceuticalCompanyRepository>();
        services.AddScoped<IPrescriptService, PrescriptRepository>();
        services.AddScoped<ISpecificationService, SpecificationRepository>();
        services.AddScoped<ITermsOfService, TermsOfServiceRepository>();
        services.AddScoped<IStatisticService, StatisticRepository>();

        services.AddScoped<IPaymentService, PaymentRepository>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}