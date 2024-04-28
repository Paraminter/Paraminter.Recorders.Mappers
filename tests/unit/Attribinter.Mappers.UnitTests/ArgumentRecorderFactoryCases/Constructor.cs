namespace Attribinter.Mappers.ArgumentRecorderFactoryCases;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactoryy()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static ArgumentRecorderFactory Target() => new();
}
