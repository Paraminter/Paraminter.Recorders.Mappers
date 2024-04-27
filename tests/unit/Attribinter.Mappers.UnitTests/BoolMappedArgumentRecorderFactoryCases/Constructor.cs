namespace Attribinter.Mappers.BoolDelegateMappedArgumentRecorderFactoryCases;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactoryy()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static BoolDelegateMappedArgumentRecorderFactory Target() => new();
}
