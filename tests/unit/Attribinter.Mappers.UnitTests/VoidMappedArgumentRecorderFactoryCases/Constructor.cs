namespace Attribinter.Mappers.VoidDelegateMappedArgumentRecorderFactoryCases;

using Xunit;

public sealed class Constructor
{
    private static VoidDelegateMappedArgumentRecorderFactory Target() => new();

    [Fact]
    public void ReturnsFactoryy()
    {
        var result = Target();

        Assert.NotNull(result);
    }
}
