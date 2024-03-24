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
    public void IArgumentRecorderFactory_T3_ServiceCanBeResolvedIfMapperProviderAdded()
    {
        ServiceCanBeResolved<IArgumentRecorderFactory<object, object, object>>(additionalConfiguration);

        static void additionalConfiguration(IServiceCollection services) => services.AddSingleton(Mock.Of<IParameterMapperProvider<object, object, object>>());
    }

    [Fact]
    public void IMappedArgumentRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IMappedArgumentRecorderFactory>();

    [Fact]
    public void IBoolDelegateMappedArgumentRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IBoolDelegateMappedArgumentRecorderFactory>();

    [Fact]
    public void IVoidDelegateMappedArgumentRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IVoidDelegateMappedArgumentRecorderFactory>();

    [AssertionMethod]
    private static void ServiceCanBeResolved<TService>() where TService : notnull => ServiceCanBeResolved<TService>((_) => { });

    [AssertionMethod]
    private static void ServiceCanBeResolved<TService>(Action<IServiceCollection> additionalConfiguration) where TService : notnull
    {
        HostBuilder host = new();

        host.ConfigureServices(static (services) => Target(services));
        host.ConfigureServices(additionalConfiguration);

        var serviceProvider = host.Build().Services;

        var service = serviceProvider.GetRequiredService<TService>();

        Assert.NotNull(service);
    }
}
