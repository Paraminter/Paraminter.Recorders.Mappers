namespace Paraminter.Recorders.Mappers.MappedArgumentDataRecorder;

using Moq;

using System;

using Xunit;

public sealed class TryRecordData
{
    [Fact]
    public void NullDataRecord_ThrowsArgumentNullException()
    {
        var fixture = FixtureFactory.Create<object, object>();

        var result = Record.Exception(() => Target(fixture, null!, Mock.Of<object>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void TrueReturningRecorder_NullData_InvokesRecorderAndReturnsTrue() => ValidRecorder_InvokesRecorderAndPropagatesReturnValue(null, true);

    [Fact]
    public void TrueReturningRecorder_NonNullData_InvokesRecorderAndReturnsTrue() => ValidRecorder_InvokesRecorderAndPropagatesReturnValue(Mock.Of<object>(), true);

    [Fact]
    public void FalseReturningRecorder_InvokesRecorderAndReturnsFalse() => ValidRecorder_InvokesRecorderAndPropagatesReturnValue(Mock.Of<object>(), false);

    private static bool Target<TRecord, TArgumentData>(
        IFixture<TRecord, TArgumentData> fixture,
        TRecord dataRecord,
        TArgumentData argumentData)
    {
        return fixture.Sut.TryRecordData(dataRecord, argumentData);
    }

    private static void ValidRecorder_InvokesRecorderAndPropagatesReturnValue(
        object? argumentData,
        bool recorderReturnValue)
    {
        var fixture = FixtureFactory.Create<object, object?>();

        var dataRecord = Mock.Of<object>();

        fixture.RecorderDelegateMock.Setup((recorder) => recorder.Invoke(dataRecord, argumentData)).Returns(recorderReturnValue);

        var result = Target(fixture, dataRecord, argumentData);

        Assert.Equal(recorderReturnValue, result);

        fixture.RecorderDelegateMock.Verify((recorder) => recorder.Invoke(dataRecord, argumentData), Times.Once());
        fixture.RecorderDelegateMock.VerifyNoOtherCalls();
    }
}
