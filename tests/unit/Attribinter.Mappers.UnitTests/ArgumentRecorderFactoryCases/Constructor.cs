namespace Attribinter.Mappers.ArgumentRecorderFactoryCases;

using Xunit;

public sealed class Constructor
{
    private static ArgumentRecorderFactory Target() => new();

    [Fact]
    public void ReturnsFactoryy()
    {
        var result = Target();

        Assert.NotNull(result);
    }
}
