namespace Paraminter.Recorders.Mappers;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactoryy()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static VoidDelegateMappedArgumentExistenceRecorderFactory Target() => new();
}
