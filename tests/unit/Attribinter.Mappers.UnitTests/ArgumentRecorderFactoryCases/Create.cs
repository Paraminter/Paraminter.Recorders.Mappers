namespace Attribinter.Mappers.ArgumentRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private static IArgumentRecorderFactory<TParameter, TRecord, TData> Target<TParameter, TRecord, TData>(IArgumentRecorderFactory factory, IParameterMapper<TParameter, TRecord, TData> mapper) => factory.WithMapper(mapper);

    [Fact]
    public void NullMapper_ThrowsArgumentNullException()
    {
        var context = FactoryContext<object, object, object>.Create();

        var exception = Record.Exception(() => Target<object, object, object>(context.Factory, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidMapper_ReturnsNotNull()
    {
        var mapper = Mock.Of<IParameterMapper<object, object, object>>();

        var context = FactoryContext<object, object, object>.Create();

        var actual = Target(context.Factory, mapper);

        Assert.NotNull(actual);
    }
}
