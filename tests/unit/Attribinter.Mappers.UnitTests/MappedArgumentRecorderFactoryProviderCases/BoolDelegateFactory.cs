namespace Attribinter.Mappers.MappedArgumentRecorderFactoryProviderCases;

using Xunit;

public sealed class BoolDelegateFactory
{
    private IBoolDelegateMappedArgumentRecorderFactory Target() => Fixture.Sut.BoolDelegateFactory;

    private readonly IProviderFixture Fixture = ProviderFixtureFactory.Create();

    [Fact]
    public void Valid_ReturnsSameAsConstructedWith()
    {
        var result = Target();

        Assert.Same(Fixture.BoolDelegateFactoryMock.Object, result);
    }
}
