namespace Paraminter.Mappers.ArgumentExistenceRecorderFactoryCases.ArgumentExistenceRecorderCases;

using Moq;

using System;

using Xunit;

public sealed class TryRecordExistence
{
    [Fact]
    public void NullParameter_ThrowsArgumentNullException()
    {
        var fixture = RecorderFixtureFactory.Create<object, object>();

        var result = Record.Exception(() => Target(fixture, null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullReturningMapper_ReturnsFalse()
    {
        var fixture = RecorderFixtureFactory.Create<object, object>();

        var parameter = Mock.Of<object>();

        fixture.MapperMock.Setup((mapper) => mapper.TryMapParameter(parameter)).Returns((IMappedArgumentExistenceRecorder<object>?)null);

        var result = Target(fixture, parameter);

        Assert.False(result);
    }

    [Fact]
    public void NonNullReturningMapper_TrueReturningRecorder_InvokesRecorderAndReturnsTrue() => NonNullReturningMapper_ValidRecorder_InvokesRecorderAndPropagatesReturnValue(true);

    [Fact]
    public void NonNullReturningMapper_FalseReturningRecorder_InvokesRecorderAndReturnsFalse() => NonNullReturningMapper_ValidRecorder_InvokesRecorderAndPropagatesReturnValue(false);

    private static bool Target<TParameter, TRecord>(
        IRecorderFixture<TParameter, TRecord> fixture,
        TParameter parameter)
        where TRecord : class
    {
        return fixture.Sut.TryRecordExistence(parameter);
    }

    [AssertionMethod]
    private static void NonNullReturningMapper_ValidRecorder_InvokesRecorderAndPropagatesReturnValue(
        bool recorderReturnValue)
    {
        var fixture = RecorderFixtureFactory.Create<object, object>();

        Mock<IMappedArgumentExistenceRecorder<object>> recorderMock = new();

        var parameter = Mock.Of<object>();

        recorderMock.Setup((recorder) => recorder.TryRecordExistence(fixture.DataRecordMock.Object)).Returns(recorderReturnValue);

        fixture.MapperMock.Setup((mapper) => mapper.TryMapParameter(parameter)).Returns(recorderMock.Object);

        var result = Target(fixture, parameter);

        Assert.Equal(recorderReturnValue, result);

        recorderMock.Verify((recorder) => recorder.TryRecordExistence(fixture.DataRecordMock.Object), Times.Once());
        recorderMock.VerifyNoOtherCalls();
    }
}
