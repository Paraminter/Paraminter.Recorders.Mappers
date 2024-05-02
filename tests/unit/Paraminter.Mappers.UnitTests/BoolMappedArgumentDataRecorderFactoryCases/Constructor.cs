namespace Paraminter.Mappers.BoolDelegateMappedArgumentDataRecorderFactoryCases;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactoryy()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static BoolDelegateMappedArgumentDataRecorderFactory Target() => new();
}
