namespace SharpAttributeParser.Mappers.SharpAttributeParserMappersServicesCases;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;

using System;

using Xunit;

public sealed class AddSharpAttributeParserMappers
{
    private static IServiceCollection Target(IServiceCollection services) => SharpAttributeParserMappersServices.AddSharpAttributeParserMappers(services);

    private readonly IServiceProvider ServiceProvider;

    public AddSharpAttributeParserMappers()
    {
        HostBuilder host = new();

        host.ConfigureServices(static (services) => Target(services));

        ServiceProvider = host.Build().Services;
    }

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
    public void IRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<IRecorderFactory>();

    [Fact]
    public void ISemanticRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<ISemanticRecorderFactory>();

    [Fact]
    public void ISyntacticRecorderFactory_ServiceCanBeResolved() => ServiceCanBeResolved<ISyntacticRecorderFactory>();

    [AssertionMethod]
    private void ServiceCanBeResolved<TService>() where TService : notnull
    {
        var service = ServiceProvider.GetRequiredService<TService>();

        Assert.NotNull(service);
    }
}
