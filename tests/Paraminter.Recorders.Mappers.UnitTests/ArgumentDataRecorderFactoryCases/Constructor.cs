namespace Paraminter.Recorders.Mappers.ArgumentDataRecorderFactoryCases;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactoryy()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static ArgumentDataRecorderFactory Target() => new();
}
