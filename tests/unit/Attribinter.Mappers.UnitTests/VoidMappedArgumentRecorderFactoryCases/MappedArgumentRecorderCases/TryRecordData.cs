namespace Attribinter.Mappers.VoidDelegateMappedArgumentRecorderFactoryCases.MappedArgumentRecorderCases;

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
    public void NullData_ReturnsTrue() => ValidRecorder_ReturnsTrue(null);

    [Fact]
    public void NonNullData_ReturnsTrue() => ValidRecorder_ReturnsTrue(Mock.Of<object>());

    [AssertionMethod]
    private static void ValidRecorder_ReturnsTrue(object? data)
    {
        var context = RecorderContext<object, object?>.Create();

        var dataRecord = Mock.Of<object>();

        var outcome = Target(context.Recorder, dataRecord, data);

        Assert.True(outcome);

        context.RecorderDelegateMock.Verify((recorder) => recorder.Invoke(dataRecord, data), Times.Once());

        context.RecorderDelegateMock.VerifyNoOtherCalls();
    }
}
