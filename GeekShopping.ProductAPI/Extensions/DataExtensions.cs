using AutoMapper;
using GeekShopping.ProductAPI.Config;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Extensions;

public static class DataExtensions
{
    private static readonly string _connection = "Server=LUCCASCARON\\SQLEXPRESS;Database=geek_shopping_product_api;Trusted_Connection=True;TrustServerCertificate=True;";

    public static IServiceCollection AddDataExtensions(this IServiceCollection services)
    {
        // Db Context
        services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(_connection));

        // Mapper
        var mappingConfig = MappingConfig.RegisterMaps();
        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }

}
