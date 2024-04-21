namespace Attribinter.Mappers.BoolDelegateMappedArgumentRecorderFactoryCases;

using Xunit;

public sealed class Constructor
{
    private static BoolDelegateMappedArgumentRecorderFactory Target() => new();

    [Fact]
    public void ReturnsFactoryy()
    {
        var result = Target();

        Assert.NotNull(result);
    }
}
