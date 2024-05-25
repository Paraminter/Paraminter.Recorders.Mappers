namespace Paraminter.Mappers.MappedArgumentDataRecorderFactoryProviderCases;

using Xunit;

public sealed class BoolDelegateFactory
{
    private readonly IProviderFixture Fixture = ProviderFixtureFactory.Create();

    [Fact]
    public void ReturnsSameAsConstructedWith()
    {
        var result = Target();

        Assert.Same(Fixture.BoolDelegateFactoryMock.Object, result);
    }

    private IBoolDelegateMappedArgumentDataRecorderFactory Target() => Fixture.Sut.BoolDelegateFactory;
}
