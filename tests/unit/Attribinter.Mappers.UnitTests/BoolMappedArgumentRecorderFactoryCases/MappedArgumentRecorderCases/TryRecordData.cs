namespace Attribinter.Mappers.BoolDelegateMappedArgumentRecorderFactoryCases.MappedArgumentRecorderCases;

using Moq;

using System;

using Xunit;

public sealed class TryRecordData
{
    private static bool Target<TRecord, TData>(IMappedArgumentRecorder<TRecord, TData> recorder, TRecord dataRecord, TData data) => recorder.TryRecordData(dataRecord, data);

    [Fact]
    public void NullDataRecord_ThrowsArgumentNullException()
    {
        var context = RecorderContext<object, object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, null!, Mock.Of<object>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void TrueReturningRecorder_NullData_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(null, true);

    [Fact]
    public void TrueReturningRecorder_NonNullData_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(Mock.Of<object>(), true);

    [Fact]
    public void FalseReturningRecorder_ReturnsFalse() => ValidRecorder_PropagatesReturnValue(Mock.Of<object>(), false);

    [AssertionMethod]
    private static void ValidRecorder_PropagatesReturnValue(object? data, bool recorderReturnValue)
    {
        var context = RecorderContext<object, object?>.Create();

        var dataRecord = Mock.Of<object>();

        context.RecorderDelegateMock.Setup(static (recorder) => recorder.Invoke(It.IsAny<object>(), It.IsAny<object>())).Returns(recorderReturnValue);

        var outcome = Target(context.Recorder, dataRecord, data);

        Assert.Equal(recorderReturnValue, outcome);

        context.RecorderDelegateMock.Verify((recorder) => recorder.Invoke(dataRecord, data), Times.Once());
        context.RecorderDelegateMock.VerifyNoOtherCalls();
    }
}
