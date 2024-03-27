namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

using Xunit;

public sealed class VoidDelegateFactory
{
    private static IVoidDelegateMappedArgumentRecorderFactory Target() => Context.Factory.VoidDelegateFactory;

    private static readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void Valid_ReturnsSameAsConstructedWith()
    {
        var actual = Target();

        Assert.Same(Context.VoidDelegateFactory, actual);
    }
}
