using System.Text;
using GrpcInterface.Interceptors;
using GrpcInterface.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GrpcInterface.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddGrpcInterfaceServices(this IServiceCollection services, string key)
    {
        services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(key)
                ),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });
        services.AddAuthorization();

        services
            .AddGrpc(options =>
            {
                options.Interceptors.Add<ErrorHandlingInterceptor>();
            })
            .AddServiceOptions<LocalizationService>(options =>
            {
                options.Interceptors.Add<AuthInterceptor>();
            })
            .AddServiceOptions<CarService>(options =>
            {
                options.Interceptors.Add<AuthInterceptor>();
            })
            .AddServiceOptions<ReservationService>(options =>
            {
                options.Interceptors.Add<AuthInterceptor>();
            })
            .AddServiceOptions<CustomerService>(options =>
            {
                options.Interceptors.Add<AuthInterceptor>();
            });
        return services;
    }
}
