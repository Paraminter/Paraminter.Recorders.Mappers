namespace Paraminter.Mappers.MappedArgumentDataRecorderFactoryProviderCases;

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

    private IVoidDelegateMappedArgumentDataRecorderFactory Target() => Fixture.Sut.VoidDelegate;
}
