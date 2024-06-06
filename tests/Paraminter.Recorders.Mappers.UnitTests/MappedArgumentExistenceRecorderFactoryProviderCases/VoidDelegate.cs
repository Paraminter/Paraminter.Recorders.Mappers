namespace Paraminter.Recorders.Mappers.MappedArgumentExistenceRecorderFactoryProviderCases;

using Xunit;

public sealed class VoidDelegate
{
    private readonly IProviderFixture Fixture = ProviderFixtureFactory.Create();

    [Fact]
    public void ReturnsFactory()
    {
        var result = Target();

        Assert.Same(Fixture.VoidDelegateMock.Object, result);
    }

    private IVoidDelegateMappedArgumentExistenceRecorderFactory Target() => Fixture.Sut.VoidDelegate;
}
