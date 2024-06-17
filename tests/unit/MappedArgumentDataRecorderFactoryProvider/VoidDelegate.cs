namespace Paraminter.Recorders.Mappers;

using Xunit;

public sealed class VoidDelegate
{
    private readonly IFixture Fixture = FixtureFactory.Create();

    [Fact]
    public void ReturnsFactory()
    {
        var result = Target();

        Assert.Same(Fixture.VoidDelegateMock.Object, result);
    }

    private IVoidDelegateMappedArgumentDataRecorderFactory Target() => Fixture.Sut.VoidDelegate;
}
