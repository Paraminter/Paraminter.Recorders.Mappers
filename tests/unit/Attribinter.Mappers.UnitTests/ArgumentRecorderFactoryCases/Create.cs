namespace Attribinter.Mappers.ArgumentRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private IArgumentRecorder<TParameter, TData> Target<TParameter, TRecord, TData>(IParameterMapper<TParameter, TRecord, TData> mapper, TRecord dataRecord) => Target(Context.Factory, mapper, dataRecord);
    private static IArgumentRecorder<TParameter, TData> Target<TParameter, TRecord, TData>(IArgumentRecorderFactory factory, IParameterMapper<TParameter, TRecord, TData> mapper, TRecord dataRecord) => factory.Create(mapper, dataRecord);

    private readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void NullMapper_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target<object, object, object>(null!, Mock.Of<object>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullDataRecord_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Factory, Mock.Of<IParameterMapper<object, object, object>>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidMapperAndRecord_ConstructedRecorderUsesMapperAndRecord()
    {
        var dataRecord = Mock.Of<object>();
        var parameter = Mock.Of<object>();
        var argument = Mock.Of<object>();

        Mock<IParameterMapper<object, object, object>> mapperMock = new() { DefaultValue = DefaultValue.Mock };

        var recorder = Target(mapperMock.Object, dataRecord);

        recorder.TryRecordData(parameter, argument);

        mapperMock.Verify((mapper) => mapper.TryMapParameter(parameter)!.TryRecordData(dataRecord, argument), Times.Once);

        mapperMock.VerifyNoOtherCalls();
    }
}
