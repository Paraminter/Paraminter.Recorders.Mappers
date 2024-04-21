namespace Attribinter.Mappers.BoolDelegateMappedArgumentRecorderFactoryCases.MappedArgumentRecorderCases;

using Moq;

using System;

using Xunit;

public sealed class TryRecordData
{
    private static bool Target<TRecord, TData>(IRecorderFixture<TRecord, TData> fixture, TRecord dataRecord, TData data) => fixture.Sut.TryRecordData(dataRecord, data);

    [Fact]
    public void NullDataRecord_ThrowsArgumentNullException()
    {
        var fixture = RecorderFixtureFactory.Create<object, object>();

        var result = Record.Exception(() => Target(fixture, null!, Mock.Of<object>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void TrueReturningRecorder_NullData_InvokesRecorderAndReturnsTrue() => ValidRecorder_InvokesRecorderAndPropagatesReturnValue(null, true);

    [Fact]
    public void TrueReturningRecorder_NonNullData_InvokesRecorderAndReturnsTrue() => ValidRecorder_InvokesRecorderAndPropagatesReturnValue(Mock.Of<object>(), true);

    [Fact]
    public void FalseReturningRecorder_InvokesRecorderAndReturnsFalse() => ValidRecorder_InvokesRecorderAndPropagatesReturnValue(Mock.Of<object>(), false);

    [AssertionMethod]
    private static void ValidRecorder_InvokesRecorderAndPropagatesReturnValue(object? data, bool recorderReturnValue)
    {
        var fixture = RecorderFixtureFactory.Create<object, object?>();

        var dataRecord = Mock.Of<object>();

        fixture.RecorderDelegateMock.Setup(static (recorder) => recorder.Invoke(It.IsAny<object>(), It.IsAny<object>())).Returns(recorderReturnValue);

        var result = Target(fixture, dataRecord, data);

        Assert.Equal(recorderReturnValue, result);

        fixture.RecorderDelegateMock.Verify((recorder) => recorder.Invoke(dataRecord, data), Times.Once());
        fixture.RecorderDelegateMock.VerifyNoOtherCalls();
    }
}
