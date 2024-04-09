namespace Attribinter.Mappers.VoidDelegateMappedArgumentRecorderFactoryCases.MappedArgumentRecorderCases;

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
    public void NullData_ReturnsTrue() => ValidRecorder_ReturnsTrue(null);

    [Fact]
    public void NonNullData_ReturnsTrue() => ValidRecorder_ReturnsTrue(Mock.Of<object>());

    [AssertionMethod]
    private static void ValidRecorder_ReturnsTrue(object? data)
    {
        var fixture = RecorderFixtureFactory.Create<object, object?>();

        var dataRecord = Mock.Of<object>();

        var result = Target(fixture, dataRecord, data);

        Assert.True(result);

        fixture.RecorderDelegateMock.Verify((recorder) => recorder.Invoke(dataRecord, data), Times.Once());
        fixture.RecorderDelegateMock.VerifyNoOtherCalls();
    }
}
