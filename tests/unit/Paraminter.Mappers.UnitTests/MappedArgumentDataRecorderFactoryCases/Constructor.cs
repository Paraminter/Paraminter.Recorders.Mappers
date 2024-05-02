namespace Paraminter.Mappers.MappedArgumentDataRecorderFactoryCases;

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
        var result = Target(Mock.Of<IMappedArgumentDataRecorderFactoryProvider>());

        Assert.NotNull(result);
    }

    private static MappedArgumentDataRecorderFactory Target(IMappedArgumentDataRecorderFactoryProvider factoryProvider) => new(factoryProvider);
}
