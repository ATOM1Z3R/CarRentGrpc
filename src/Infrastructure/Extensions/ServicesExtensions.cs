using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Caching;
using Infrastructure.EfCore;
using Infrastructure.EfCore.PgRepos;
using Infrastructure.Services;
using Application.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        services.AddMemoryCache();

        services.AddDbContext<DataBaseContext>(
            ctx => ctx.UseNpgsql(connectionString)
        );

        services.AddScoped<IEmployeeRepo, PgEmployeeRepo>();
        
        services
            .AddScoped<IReservationRepo, CacheReservationRepo>()
            .AddScoped<PgReservationRepo>();
        services
            .AddScoped<ICustomerRepo, CacheCustomerRepo>()
            .AddScoped<PgCustomerRepo>();
        services
            .AddScoped<ICarRepo, CacheCarRepo>()
            .AddScoped<PgCarRepo>();
        services
            .AddScoped<ILocalizationRepo, CacheLocalizationRepo>()
            .AddScoped<PgLocalizationRepo>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
