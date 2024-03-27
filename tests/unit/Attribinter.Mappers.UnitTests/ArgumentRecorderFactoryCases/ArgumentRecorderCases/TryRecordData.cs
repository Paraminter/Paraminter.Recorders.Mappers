namespace Attribinter.Mappers.ArgumentRecorderFactoryCases.ArgumentRecorderCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;

using Xunit;

public sealed class TryRecordData
{
    private static bool Target<TParameter, TData>(IArgumentRecorder<TParameter, TData> recorder, TParameter parameter, TData data) => recorder.TryRecordData(parameter, data);

    [Fact]
    public void NullParameter_ThrowsArgumentNullException()
    {
        var context = RecorderContext<object, object, object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, null!, Mock.Of<object>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullData_ThrowsArgumentNullException()
    {
        var context = RecorderContext<object, object, object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, Mock.Of<ITypeParameterSymbol>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullReturningMapper_ReturnsFalse()
    {
        var context = RecorderContext<object, object, object>.Create();

        var parameter = Mock.Of<object>();
        var data = Mock.Of<object>();

        context.MapperMock.Setup(static (mapper) => mapper.TryMapParameter(It.IsAny<object>())).Returns((IMappedArgumentRecorder<object, object>?)null);

        var outcome = Target(context.Recorder, parameter, data);

        Assert.False(outcome);

        context.MapperMock.Verify((mapper) => mapper.TryMapParameter(parameter), Times.Once);

        context.MapperMock.VerifyNoOtherCalls();
    }

    [Fact]
    public void NonNullReturningMapper_TrueReturningRecorder_ReturnsTrue() => NonNullReturningMapper_ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void NonNullReturningMapper_FalseReturningRecorder_ReturnsFalse() => NonNullReturningMapper_ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private static void NonNullReturningMapper_ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var context = RecorderContext<object, object, object>.Create();

        var parameter = Mock.Of<object>();
        var data = Mock.Of<object>();

        context.MapperMock.Setup(static (mapper) => mapper.TryMapParameter(It.IsAny<object>())!.TryRecordData(It.IsAny<object>(), It.IsAny<object>())).Returns(recorderReturnValue);

        var outcome = Target(context.Recorder, parameter, data);

        Assert.Equal(recorderReturnValue, outcome);

        context.MapperMock.Verify((mapper) => mapper.TryMapParameter(parameter)!.TryRecordData(context.DataRecord, data), Times.Once);

        context.MapperMock.VerifyNoOtherCalls();
    }
}
