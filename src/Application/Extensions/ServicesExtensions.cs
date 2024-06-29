using Application.Query.Localization.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this  IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetByCityHandler).Assembly));
        return services;
    }
}
