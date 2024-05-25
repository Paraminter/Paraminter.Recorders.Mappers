namespace Paraminter.Mappers.MappedArgumentDataRecorderFactoryProviderCases;

using Xunit;

public sealed class VoidDelegateFactory
{
    private readonly IProviderFixture Fixture = ProviderFixtureFactory.Create();

    [Fact]
    public void ReturnsSameAsConstructedWith()
    {
        var result = Target();

        Assert.Same(Fixture.VoidDelegateFactoryMock.Object, result);
    }

    private IVoidDelegateMappedArgumentDataRecorderFactory Target() => Fixture.Sut.VoidDelegateFactory;
}
