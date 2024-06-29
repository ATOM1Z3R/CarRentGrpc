using Infrastructure.EfCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace tests.IntergrationTests;

public abstract class BaseIntegrationTest : IClassFixture<BaseIntegrationTestWebAppFactory>
{
    public readonly IServiceScope _scope;
    protected ISender Sender;
    public DataBaseContext DbContext { get; }

    protected BaseIntegrationTest(BaseIntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();

        DbContext = _scope.ServiceProvider.GetRequiredService<DataBaseContext>();
    }
}