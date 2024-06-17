namespace Paraminter.Recorders.Mappers;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private readonly IFixture Fixture = FixtureFactory.Create();

    [Fact]
    public void NullRecorderDelegate_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<object, object>(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidRecorderDelegate_ReturnsRecorder()
    {
        var result = Target(Mock.Of<DVoidArgumentDataRecorder<object, object>>());

        Assert.NotNull(result);
    }

    private IMappedArgumentDataRecorder<TRecord, TArgumentData> Target<TRecord, TArgumentData>(
        DVoidArgumentDataRecorder<TRecord, TArgumentData> recorderDelegate)
    {
        return Fixture.Sut.Create(recorderDelegate);
    }
}
