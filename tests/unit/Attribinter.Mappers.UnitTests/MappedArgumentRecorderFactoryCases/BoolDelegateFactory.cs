namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

using Xunit;

public sealed class BoolDelegateFactory
{
    private static IBoolDelegateMappedArgumentRecorderFactory Target() => Context.Factory.BoolDelegateFactory;

    private static readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void Valid_ReturnsSameAsConstructedWith()
    {
        var actual = Target();

        Assert.Same(Context.BoolDelegateFactory, actual);
    }
}
