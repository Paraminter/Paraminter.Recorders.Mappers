namespace Attribinter.Mappers.AttribinterMappersServicesCases;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;

using System;

using Xunit;

public sealed class AddAttribinterMappers
{
    private static IServiceCollection Target(IServiceCollection services) => AttribinterMappersServices.AddAttribinterMappers(services);

    [Fact]
    public void NullServiceCollection_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidServiceCollection_ReturnsSameServiceCollection()
    {
        var serviceCollection = Mock.Of<IServiceCollection>();

        var actual = Target(serviceCollection);

        Assert.Same(serviceCollection, actual);
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

    [AssertionMethod]
    private static void ServiceCanBeResolved<TService>() where TService : notnull
    {
        HostBuilder host = new();

        host.ConfigureServices(configureServices);

        var serviceProvider = host.Build().Services;

        var service = serviceProvider.GetRequiredService<TService>();

        Assert.NotNull(service);

        static void configureServices(IServiceCollection services) => Target(services);
    }
}
