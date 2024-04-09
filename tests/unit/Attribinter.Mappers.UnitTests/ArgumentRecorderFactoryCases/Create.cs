namespace Attribinter.Mappers.ArgumentRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private IArgumentRecorder<TParameter, TData> Target<TParameter, TRecord, TData>(IParameterMapper<TParameter, TRecord, TData> mapper, TRecord dataRecord) => Fixture.Sut.Create(mapper, dataRecord);

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
        var result = Record.Exception(() => Target(Mock.Of<IParameterMapper<object, object, object>>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }
}
