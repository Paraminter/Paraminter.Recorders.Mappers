namespace Paraminter.Mappers.ParaminterMappersServicesCases;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;

using System;

using Xunit;

public sealed class AddParaminterMappers
{
    [Fact]
    public void NullServiceCollection_ArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidServiceCollection_ReturnsSameServiceCollection()
    {
        var services = Mock.Of<IServiceCollection>();

        var result = Target(services);

        Assert.Same(services, result);
    }

    [Fact]
    public void IArgumentDataRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IArgumentDataRecorderFactory>();

    [Fact]
    public void IMappedArgumentDataRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IMappedArgumentDataRecorderFactory>();

    [Fact]
    public void IMappedArgumentDataRecorderFactoryProvider_ServiceCanBeResolved() => ServiceCanBeResolved<IMappedArgumentDataRecorderFactoryProvider>();

    [Fact]
    public void IBoolDelegateMappedArgumentDataRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IBoolDelegateMappedArgumentDataRecorderFactory>();

    [Fact]
    public void IVoidDelegateMappedArgumentDataRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IVoidDelegateMappedArgumentDataRecorderFactory>();

    private static IServiceCollection Target(IServiceCollection services) => ParaminterMappersServices.AddParaminterMappers(services);

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
