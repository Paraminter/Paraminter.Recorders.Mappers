namespace Paraminter.Recorders.Mappers.MappedArgumentExistenceRecorderFactoryProviderCases;

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

    private IBoolDelegateMappedArgumentExistenceRecorderFactory Target() => Fixture.Sut.BoolDelegate;
}
