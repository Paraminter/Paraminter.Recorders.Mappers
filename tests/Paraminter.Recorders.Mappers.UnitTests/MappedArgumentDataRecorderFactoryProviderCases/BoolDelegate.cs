namespace Paraminter.Recorders.Mappers.MappedArgumentDataRecorderFactoryProviderCases;

using Xunit;

public sealed class BoolDelegate
{
    private readonly IProviderFixture Fixture = ProviderFixtureFactory.Create();

    [Fact]
    public void ReturnsFactory()
    {
        var result = Target();

        Assert.Same(Fixture.BoolDelegateMock.Object, result);
    }

    private IBoolDelegateMappedArgumentDataRecorderFactory Target() => Fixture.Sut.BoolDelegate;
}
