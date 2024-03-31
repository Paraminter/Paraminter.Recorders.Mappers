namespace Attribinter.Mappers.MappedArgumentRecorderFactoryProviderCases;

using Xunit;

public sealed class BoolDelegateFactory
{
    private static IBoolDelegateMappedArgumentRecorderFactory Target() => Context.Provider.BoolDelegateFactory;

    private static readonly ProviderContext Context = ProviderContext.Create();

    [Fact]
    public void Valid_ReturnsSameAsConstructedWith()
    {
        var actual = Target();

        Assert.Same(Context.BoolDelegateFactory, actual);
    }
}
