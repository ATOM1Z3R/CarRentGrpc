using GrpcInterface.Services;
using Infrastructure.Extensions;
using Application.Extensions;
using GrpcInterface.Extensions;
using Infrastructure.EfCore;
using GrpcInterface.SampleData;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["Database:ConnectionString"];
var JwtKey = builder.Configuration["Jwt:Key"];

builder.Services
    .AddInfrastructureServices(connectionString ?? "")
    .AddApplicationServices()
    .AddGrpcInterfaceServices(JwtKey ?? "");

var app = builder.Build();

InitSampleData.InitData(app.Services.GetRequiredService<DataBaseContext>());

app.UseRouting()
   .UseAuthentication()
   .UseAuthorization()
   .UseEndpoints(endpoints =>
    {
        endpoints.MapGrpcService<AuthService>();
        endpoints.MapGrpcService<LocalizationService>();
        endpoints.MapGrpcService<CustomerService>();
        endpoints.MapGrpcService<ReservationService>();
        endpoints.MapGrpcService<CarService>();
    });

app.Run();

public partial class Program { }
