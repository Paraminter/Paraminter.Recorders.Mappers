namespace Attribinter.Mappers.VoidDelegateMappedArgumentRecorderFactoryCases;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactoryy()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static VoidDelegateMappedArgumentRecorderFactory Target() => new();
}
