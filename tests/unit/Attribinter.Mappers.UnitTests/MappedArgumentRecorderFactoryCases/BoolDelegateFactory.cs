namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

using Xunit;

public sealed class BoolDelegateFactory
{
    private IBoolDelegateMappedArgumentRecorderFactory Target() => Target(Context.Factory);
    private static IBoolDelegateMappedArgumentRecorderFactory Target(IMappedArgumentRecorderFactory factory) => factory.BoolDelegateFactory;

    private readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void Valid_ReturnsSameAsConstructedWith()
    {
        var actual = Target();

        Assert.Same(Context.BoolDelegateFactory, actual);
    }
}
