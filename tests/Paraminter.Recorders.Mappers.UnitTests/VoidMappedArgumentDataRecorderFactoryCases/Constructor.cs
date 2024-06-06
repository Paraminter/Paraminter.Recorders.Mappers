namespace Paraminter.Mappers.VoidDelegateMappedArgumentDataRecorderFactoryCases;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactoryy()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static VoidDelegateMappedArgumentDataRecorderFactory Target() => new();
}
