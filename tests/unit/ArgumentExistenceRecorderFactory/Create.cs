namespace Paraminter.Recorders.Mappers;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private readonly IFixture Fixture = FixtureFactory.Create();

    [Fact]
    public void NullMapper_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<object, object>(null!, Mock.Of<object>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullDataRecord_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(Mock.Of<IArgumentExistenceRecorderMapper<object, object>>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsRecorder()
    {
        var result = Target(Mock.Of<IArgumentExistenceRecorderMapper<object, object>>(), Mock.Of<object>());

        Assert.NotNull(result);
    }

    private IArgumentExistenceRecorder<TParameter> Target<TParameter, TRecord>(
        IArgumentExistenceRecorderMapper<TParameter, TRecord> mapper,
        TRecord dataRecord)
    {
        return Fixture.Sut.Create(mapper, dataRecord);
    }
}
