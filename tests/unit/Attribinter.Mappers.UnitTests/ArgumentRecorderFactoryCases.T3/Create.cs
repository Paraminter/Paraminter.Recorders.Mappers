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
    public void ValidRecord_ConstructsRecorder()
    {
        var dataRecord = Mock.Of<object>();

        var recorder = Mock.Of<IArgumentRecorder<object, object>>();
        var mapper = Mock.Of<IParameterMapper<object, object, object>>();

        var context = FactoryContext<object, object, object>.Create();

        context.InnerFactoryMock.Setup(static (factory) => factory.Create(It.IsAny<IParameterMapper<object, object, object>>(), It.IsAny<object>())).Returns(recorder);
        context.MapperProviderMock.Setup(static (provider) => provider.Mapper).Returns(mapper);

        var actual = Target(context.Factory, dataRecord);

        Assert.Same(recorder, actual);

        context.InnerFactoryMock.Verify((factory) => factory.Create(mapper, dataRecord), Times.Once());
        context.InnerFactoryMock.VerifyNoOtherCalls();

        context.MapperProviderMock.Verify((provider) => provider.Mapper, Times.Once());
        context.MapperProviderMock.VerifyNoOtherCalls();
    }
}
