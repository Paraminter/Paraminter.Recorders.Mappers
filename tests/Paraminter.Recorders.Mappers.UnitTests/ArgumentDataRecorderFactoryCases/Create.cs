namespace Paraminter.Recorders.Mappers.ArgumentDataRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private readonly IFactoryFixture Fixture = FactoryFixtureFactory.Create();

    [Fact]
    public void NullMapper_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<object, object, object>(null!, Mock.Of<object>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullDataRecord_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(Mock.Of<IArgumentDataRecorderMapper<object, object, object>>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsRecorder()
    {
        var result = Target(Mock.Of<IArgumentDataRecorderMapper<object, object, object>>(), Mock.Of<object>());

        Assert.NotNull(result);
    }

    private IArgumentDataRecorder<TParameter, TArgumentData> Target<TParameter, TRecord, TArgumentData>(
        IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData> mapper,
        TRecord dataRecord)
    {
        return Fixture.Sut.Create(mapper, dataRecord);
    }
}
