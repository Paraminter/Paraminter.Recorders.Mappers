namespace Paraminter.Mappers.BoolDelegateMappedArgumentExistenceRecorderFactoryCases;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactoryy()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static BoolDelegateMappedArgumentExistenceRecorderFactory Target() => new();
}
