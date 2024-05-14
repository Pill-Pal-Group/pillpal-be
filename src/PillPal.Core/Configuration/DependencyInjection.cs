using Microsoft.Extensions.DependencyInjection;
using PillPal.Core.Mappings;

namespace PillPal.Core.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperConfigure).Assembly);
        return services;
    }

    //public static IServiceCollection AddFluentValidator(this IServiceCollection services)
    //{
    //    services.AddFluentValidation();
    //}
}
