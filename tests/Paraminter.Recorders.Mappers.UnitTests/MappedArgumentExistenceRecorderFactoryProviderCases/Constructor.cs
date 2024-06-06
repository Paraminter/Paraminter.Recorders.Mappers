namespace Paraminter.Mappers.MappedArgumentExistenceRecorderFactoryProviderCases;

using Moq;

using System;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void NullBoolDelegateFactory_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!, Mock.Of<IVoidDelegateMappedArgumentExistenceRecorderFactory>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullVoidDelegateFactory_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(Mock.Of<IBoolDelegateMappedArgumentExistenceRecorderFactory>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsProvider()
    {
        var result = Target(Mock.Of<IBoolDelegateMappedArgumentExistenceRecorderFactory>(), Mock.Of<IVoidDelegateMappedArgumentExistenceRecorderFactory>());

        Assert.NotNull(result);
    }

    private static MappedArgumentExistenceRecorderFactoryProvider Target(
        IBoolDelegateMappedArgumentExistenceRecorderFactory boolDelegateFactory,
        IVoidDelegateMappedArgumentExistenceRecorderFactory voidDelegateFactory)
    {
        return new(boolDelegateFactory, voidDelegateFactory);
    }
}
