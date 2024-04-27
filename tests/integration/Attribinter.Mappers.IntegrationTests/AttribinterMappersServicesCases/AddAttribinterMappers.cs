namespace Attribinter.Mappers.AttribinterMappersServicesCases;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;

using System;

using Xunit;

public sealed class AddAttribinterMappers
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
    public void IArgumentRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IArgumentRecorderFactory>();

    [Fact]
    public void IMappedArgumentRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IMappedArgumentRecorderFactory>();

    [Fact]
    public void IMappedArgumentRecorderFactoryProvider_ServiceCanBeResolved() => ServiceCanBeResolved<IMappedArgumentRecorderFactoryProvider>();

    [Fact]
    public void IBoolDelegateMappedArgumentRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IBoolDelegateMappedArgumentRecorderFactory>();

    [Fact]
    public void IVoidDelegateMappedArgumentRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IVoidDelegateMappedArgumentRecorderFactory>();

    private static IServiceCollection Target(IServiceCollection services) => AttribinterMappersServices.AddAttribinterMappers(services);

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
