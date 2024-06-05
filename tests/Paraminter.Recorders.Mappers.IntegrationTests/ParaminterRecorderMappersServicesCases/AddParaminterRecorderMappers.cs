namespace Paraminter.Mappers.ParaminterRecorderMappersServicesCases;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Xunit;

public sealed class AddParaminterRecorderMappers
{
    [Fact]
    public void IArgumentDataRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IArgumentDataRecorderFactory>();

    [Fact]
    public void IArgumentExistenceRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IArgumentExistenceRecorderFactory>();

    [Fact]
    public void IMappedArgumentDataRecorderFactoryProvider_ServiceCanBeResolved() => ServiceCanBeResolved<IMappedArgumentDataRecorderFactoryProvider>();

    [Fact]
    public void IBoolDelegateMappedArgumentDataRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IBoolDelegateMappedArgumentDataRecorderFactory>();

    [Fact]
    public void IVoidDelegateMappedArgumentDataRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IVoidDelegateMappedArgumentDataRecorderFactory>();

    [Fact]
    public void IMappedArgumentExistenceRecorderFactoryProvider_ServiceCanBeResolved() => ServiceCanBeResolved<IMappedArgumentExistenceRecorderFactoryProvider>();

    [Fact]
    public void IBoolDelegateMappedArgumentExistenceRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IBoolDelegateMappedArgumentExistenceRecorderFactory>();

    [Fact]
    public void IVoidDelegateMappedArgumentExistenceRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IVoidDelegateMappedArgumentExistenceRecorderFactory>();

    private static void Target(
        IServiceCollection services)
    {
        ParaminterRecorderMappersServices.AddParaminterRecorderMappers(services);
    }

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
