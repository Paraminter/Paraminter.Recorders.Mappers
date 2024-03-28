namespace Attribinter.Mappers.ArgumentRecorderFactoryCases.T3;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private static IArgumentRecorder<TParameter, TData> Target<TParameter, TRecord, TData>(IArgumentRecorderFactory<TParameter, TRecord, TData> factory, TRecord dataRecord) => factory.Create(dataRecord);

    [Fact]
    public void NullDataRecord_ThrowsArgumentNullException()
    {
        var context = FactoryContext<object, object, object>.Create();

        var exception = Record.Exception(() => Target(context.Factory, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidDataRecord_ReturnsNotNull()
    {
        var dataRecord = Mock.Of<object>();

        var context = FactoryContext<object, object, object>.Create();

        var actual = Target(context.Factory, dataRecord);

        Assert.NotNull(actual);
    }
}
