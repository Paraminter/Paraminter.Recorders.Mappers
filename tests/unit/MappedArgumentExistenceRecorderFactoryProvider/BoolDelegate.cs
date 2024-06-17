namespace Paraminter.Recorders.Mappers;

using Xunit;

public sealed class BoolDelegate
{
    private readonly IFixture Fixture = FixtureFactory.Create();

    [Fact]
    public void ReturnsFactory()
    {
        var result = Target();

        Assert.Same(Fixture.BoolDelegateMock.Object, result);
    }

    private IBoolDelegateMappedArgumentExistenceRecorderFactory Target() => Fixture.Sut.BoolDelegate;
}
