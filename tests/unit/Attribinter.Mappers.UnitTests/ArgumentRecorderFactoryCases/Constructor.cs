namespace Attribinter.Mappers.ArgumentRecorderFactoryCases;

using System;

using Xunit;

public sealed class Constructor
{
    private static ArgumentRecorderFactory<TParameter, TRecord, TData> Target<TParameter, TRecord, TData>(IParameterMapper<TParameter, TRecord, TData> mapper) => new(mapper);

    [Fact]
    public void NullMapper_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<object, object, object>(null!));

        Assert.IsType<ArgumentNullException>(result);
    }
}
