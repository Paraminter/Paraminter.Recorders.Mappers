namespace Paraminter.Recorders.Mappers;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsFactory()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static ArgumentDataRecorderFactory Target() => new();
}
