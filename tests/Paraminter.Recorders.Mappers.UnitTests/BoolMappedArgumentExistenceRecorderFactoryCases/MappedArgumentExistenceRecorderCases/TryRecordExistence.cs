namespace Paraminter.Recorders.Mappers.BoolDelegateMappedArgumentExistenceRecorderFactoryCases.MappedArgumentExistenceRecorderCases;

using Moq;

using System;

using Xunit;

public sealed class TryRecordExistence
{
    [Fact]
    public void NullDataRecord_ThrowsArgumentNullException()
    {
        var fixture = RecorderFixtureFactory.Create<object>();

        var result = Record.Exception(() => Target(fixture, null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void TrueReturningRecorder_InvokesRecorderAndReturnsTrue() => ValidRecorder_InvokesRecorderAndPropagatesReturnValue(true);

    [Fact]
    public void FalseReturningRecorder_InvokesRecorderAndReturnsFalse() => ValidRecorder_InvokesRecorderAndPropagatesReturnValue(false);

    private static bool Target<TRecord>(
        IRecorderFixture<TRecord> fixture,
        TRecord dataRecord)
    {
        return fixture.Sut.TryRecordExistence(dataRecord);
    }

    [AssertionMethod]
    private static void ValidRecorder_InvokesRecorderAndPropagatesReturnValue(
        bool recorderReturnValue)
    {
        var fixture = RecorderFixtureFactory.Create<object?>();

        var dataRecord = Mock.Of<object>();

        fixture.RecorderDelegateMock.Setup((recorder) => recorder.Invoke(dataRecord)).Returns(recorderReturnValue);

        var result = Target(fixture, dataRecord);

        Assert.Equal(recorderReturnValue, result);

        fixture.RecorderDelegateMock.Verify((recorder) => recorder.Invoke(dataRecord), Times.Once());
        fixture.RecorderDelegateMock.VerifyNoOtherCalls();
    }
}
