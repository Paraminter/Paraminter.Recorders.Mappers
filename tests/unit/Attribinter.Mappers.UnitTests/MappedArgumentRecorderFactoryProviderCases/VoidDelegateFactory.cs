namespace Attribinter.Mappers.MappedArgumentRecorderFactoryProviderCases;

using Xunit;

public sealed class VoidDelegateFactory
{
    private static IVoidDelegateMappedArgumentRecorderFactory Target() => Context.Provider.VoidDelegateFactory;

    private static readonly ProviderContext Context = ProviderContext.Create();

    [Fact]
    public void Valid_ReturnsSameAsConstructedWith()
    {
        var actual = Target();

        Assert.Same(Context.VoidDelegateFactory, actual);
    }
}
