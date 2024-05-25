namespace Paraminter.Mappers.ParaminterMappersServicesCases;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Xunit;

public sealed class AddParaminterMappers
{
    [Fact]
    public void IArgumentDataRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IArgumentDataRecorderFactory>();

    [Fact]
    public void IMappedArgumentDataRecorderFactoryProvider_ServiceCanBeResolved() => ServiceCanBeResolved<IMappedArgumentDataRecorderFactoryProvider>();

    [Fact]
    public void IBoolDelegateMappedArgumentDataRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IBoolDelegateMappedArgumentDataRecorderFactory>();

    [Fact]
    public void IVoidDelegateMappedArgumentDataRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IVoidDelegateMappedArgumentDataRecorderFactory>();

    private static void Target(IServiceCollection services) => ParaminterMappersServices.AddParaminterMappers(services);

    [AssertionMethod]
    private static void ServiceCanBeResolved<TService>()
        where TService : notnull
    {
        HostBuilder host = new();

        host.ConfigureServices(static (services) => Target(services));

        var serviceProvider = host.Build().Services;

        var result = serviceProvider.GetRequiredService<TService>();

        Assert.NotNull(result);
    }
}
