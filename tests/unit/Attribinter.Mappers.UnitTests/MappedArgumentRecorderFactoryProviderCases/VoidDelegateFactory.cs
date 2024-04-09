namespace Attribinter.Mappers.MappedArgumentRecorderFactoryProviderCases;

using Xunit;

public sealed class VoidDelegateFactory
{
    private IVoidDelegateMappedArgumentRecorderFactory Target() => Fixture.Sut.VoidDelegateFactory;

    private readonly IProviderFixture Fixture = ProviderFixtureFactory.Create();

    [Fact]
    public void Valid_ReturnsSameAsConstructedWith()
    {
        var result = Target();

        Assert.Same(Fixture.VoidDelegateFactoryMock.Object, result);
    }
}
