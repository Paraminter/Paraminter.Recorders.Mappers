namespace Attribinter.Mappers.ArgumentRecorderFactoryCases;

using System;

using Xunit;

public sealed class Create
{
    private static IArgumentRecorder<TParameter, TData> Target<TParameter, TRecord, TData>(IFactoryFixture<TParameter, TRecord, TData> fixture, TRecord dataRecord) => fixture.Sut.Create(dataRecord);

    [Fact]
    public void NullDataRecord_ThrowsArgumentNullException()
    {
        var fixture = FactoryFixtureFactory.Create<object, object, object>();

        var result = Record.Exception(() => Target(fixture, null!));

        Assert.IsType<ArgumentNullException>(result);
    }
}
