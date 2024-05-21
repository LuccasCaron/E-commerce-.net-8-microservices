using GeekShopping.ProductAPI.Repository;

namespace GeekShopping.ProductAPI.Extensions;

public static class ServicesExtensions
{

    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;

    }

}
