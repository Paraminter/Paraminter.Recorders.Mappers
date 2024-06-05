namespace Paraminter.Mappers.ArgumentExistenceRecorderFactoryCases;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactoryy()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static ArgumentExistenceRecorderFactory Target() => new();
}
