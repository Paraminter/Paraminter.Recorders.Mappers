namespace Paraminter.Mappers.MappedArgumentDataRecorderFactoryProviderCases;

using Moq;

using System;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void NullBoolDelegateFactory_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!, Mock.Of<IVoidDelegateMappedArgumentDataRecorderFactory>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullVoidDelegateFactory_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(Mock.Of<IBoolDelegateMappedArgumentDataRecorderFactory>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsProvider()
    {
        var result = Target(Mock.Of<IBoolDelegateMappedArgumentDataRecorderFactory>(), Mock.Of<IVoidDelegateMappedArgumentDataRecorderFactory>());

        Assert.NotNull(result);
    }

    private static MappedArgumentDataRecorderFactoryProvider Target(IBoolDelegateMappedArgumentDataRecorderFactory boolDelegateFactory, IVoidDelegateMappedArgumentDataRecorderFactory voidDelegateFactory) => new(boolDelegateFactory, voidDelegateFactory);
}
