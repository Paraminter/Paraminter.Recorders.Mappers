namespace Paraminter.Mappers.VoidDelegateMappedArgumentDataRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private readonly IFactoryFixture Fixture = FactoryFixtureFactory.Create();

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

    private IMappedArgumentDataRecorder<TRecord, TArgumentData> Target<TRecord, TArgumentData>(DVoidArgumentDataRecorder<TRecord, TArgumentData> recorderDelegate) => Fixture.Sut.Create(recorderDelegate);
}
