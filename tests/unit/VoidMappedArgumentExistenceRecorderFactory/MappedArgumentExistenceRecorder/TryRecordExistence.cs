namespace Paraminter.Recorders.Mappers.MappedArgumentExistenceRecorder;

using Moq;

using System;

using Xunit;

public sealed class TryRecordExistence
{
    [Fact]
    public void NullDataRecord_ThrowsArgumentNullException()
    {
        var fixture = FixtureFactory.Create<object>();

        var result = Record.Exception(() => Target(fixture, null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void InvokesRecorderAndReturnsTrue()
    {
        var fixture = FixtureFactory.Create<object?>();

        var dataRecord = Mock.Of<object>();

        fixture.RecorderDelegateMock.Setup((recorder) => recorder.Invoke(dataRecord));

        var result = Target(fixture, dataRecord);

        Assert.True(result);

        fixture.RecorderDelegateMock.Verify((recorder) => recorder.Invoke(dataRecord), Times.Once());
        fixture.RecorderDelegateMock.VerifyNoOtherCalls();
    }

    private static bool Target<TRecord>(
        IFixture<TRecord> fixture,
        TRecord dataRecord)
    {
        return fixture.Sut.TryRecordExistence(dataRecord);
    }
}
