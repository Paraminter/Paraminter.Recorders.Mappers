namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void NullFactoryProvider_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsFactory()
    {
        var result = Target(Mock.Of<IMappedArgumentRecorderFactoryProvider>());

        Assert.NotNull(result);
    }

    private static MappedArgumentRecorderFactory Target(IMappedArgumentRecorderFactoryProvider factoryProvider) => new(factoryProvider);
}
