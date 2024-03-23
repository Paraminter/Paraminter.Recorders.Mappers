namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

using Xunit;

public sealed class VoidDelegateFactory
{
    private IVoidDelegateMappedArgumentRecorderFactory Target() => Target(Context.Factory);
    private static IVoidDelegateMappedArgumentRecorderFactory Target(IMappedArgumentRecorderFactory factory) => factory.VoidDelegateFactory;

    private readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void Valid_ReturnsSameAsConstructedWith()
    {
        var actual = Target();

        Assert.Same(Context.VoidDelegateFactory, actual);
    }
}
