namespace Attribinter.Mappers.MappedArgumentRecorderFactoryProviderCases;

using Moq;

using System;

using Xunit;

public sealed class Constructor
{
    private static MappedArgumentRecorderFactoryProvider Target(IBoolDelegateMappedArgumentRecorderFactory boolDelegateFactory, IVoidDelegateMappedArgumentRecorderFactory voidDelegateFactory) => new(boolDelegateFactory, voidDelegateFactory);

    [Fact]
    public void NullBoolDelegateFactory_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!, Mock.Of<IVoidDelegateMappedArgumentRecorderFactory>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullVoidDelegateFactory_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(Mock.Of<IBoolDelegateMappedArgumentRecorderFactory>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }
}
