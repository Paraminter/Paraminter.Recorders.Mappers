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
    public void NullData_InvokesRecorderAndReturnsTrue() => ValidRecorder_InvokesRecorderAndReturnsTrue(null);

    [Fact]
    public void NonNullData_InvokesRecorderAndReturnsTrue() => ValidRecorder_InvokesRecorderAndReturnsTrue(Mock.Of<object>());

    private static bool Target<TRecord, TArgumentData>(
        IFixture<TRecord, TArgumentData> fixture,
        TRecord dataRecord,
        TArgumentData argumentData)
    {
        return fixture.Sut.TryRecordData(dataRecord, argumentData);
    }

    private static void ValidRecorder_InvokesRecorderAndReturnsTrue(
        object? argumentData)
    {
        var fixture = FixtureFactory.Create<object, object?>();

        var dataRecord = Mock.Of<object>();

        fixture.RecorderDelegateMock.Setup((recorder) => recorder.Invoke(dataRecord, argumentData));

        var result = Target(fixture, dataRecord, argumentData);

        Assert.True(result);

        fixture.RecorderDelegateMock.Verify((recorder) => recorder.Invoke(dataRecord, argumentData), Times.Once());
        fixture.RecorderDelegateMock.VerifyNoOtherCalls();
    }
}
