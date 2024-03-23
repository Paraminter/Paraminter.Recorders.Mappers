namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases.MappedArgumentRecorderCases;

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
    public void NullData_ThrowsArgumentNullException()
    {
        var context = RecorderContext<object, object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, Mock.Of<object>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void TrueReturningRecorder_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void FalseReturningRecorder_ReturnsFalse() => ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private static void ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var context = RecorderContext<object, object>.Create();

        var dataRecord = Mock.Of<object>();
        var data = Mock.Of<object>();

        context.RecorderDelegateMock.Setup(static (recorder) => recorder.Invoke(It.IsAny<object>(), It.IsAny<object>())).Returns(recorderReturnValue);

        var outcome = Target(context.Recorder, dataRecord, data);

        Assert.Equal(recorderReturnValue, outcome);

        context.RecorderDelegateMock.Verify((recorder) => recorder.Invoke(dataRecord, data), Times.Once());

        context.RecorderDelegateMock.VerifyNoOtherCalls();
    }
}
