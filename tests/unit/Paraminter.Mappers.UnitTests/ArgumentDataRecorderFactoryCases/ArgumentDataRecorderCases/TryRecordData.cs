namespace Paraminter.Mappers.ArgumentDataRecorderFactoryCases.ArgumentDataRecorderCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;

using Xunit;

public sealed class TryRecordData
{
    [Fact]
    public void NullParameter_ThrowsArgumentNullException()
    {
        var fixture = RecorderFixtureFactory.Create<object, object, object>();

        var result = Record.Exception(() => Target(fixture, null!, Mock.Of<object>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullData_ThrowsArgumentNullException()
    {
        var fixture = RecorderFixtureFactory.Create<object, object, object>();

        var result = Record.Exception(() => Target(fixture, Mock.Of<ITypeParameterSymbol>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullReturningMapper_ReturnsFalse()
    {
        var fixture = RecorderFixtureFactory.Create<object, object, object>();

        var parameter = Mock.Of<object>();
        var data = Mock.Of<object>();

        fixture.MapperMock.Setup((mapper) => mapper.TryMapParameter(parameter)).Returns((IMappedArgumentDataRecorder<object, object>?)null);

        var result = Target(fixture, parameter, data);

        Assert.False(result);
    }

    [Fact]
    public void NonNullReturningMapper_TrueReturningRecorder_InvokesRecorderAndReturnsTrue() => NonNullReturningMapper_ValidRecorder_InvokesRecorderAndPropagatesReturnValue(true);

    [Fact]
    public void NonNullReturningMapper_FalseReturningRecorder_InvokesRecorderAndReturnsFalse() => NonNullReturningMapper_ValidRecorder_InvokesRecorderAndPropagatesReturnValue(false);

    private static bool Target<TParameter, TRecord, TArgumentData>(IRecorderFixture<TParameter, TRecord, TArgumentData> fixture, TParameter parameter, TArgumentData argumentData)
        where TRecord : class
    {
        return fixture.Sut.TryRecordData(parameter, argumentData);
    }

    [AssertionMethod]
    private static void NonNullReturningMapper_ValidRecorder_InvokesRecorderAndPropagatesReturnValue(bool recorderReturnValue)
    {
        var fixture = RecorderFixtureFactory.Create<object, object, object>();

        Mock<IMappedArgumentDataRecorder<object, object>> recorderMock = new();

        recorderMock.Setup(static (recorder) => recorder.TryRecordData(It.IsAny<object>(), It.IsAny<object>())).Returns(recorderReturnValue);

        var parameter = Mock.Of<object>();
        var argumentData = Mock.Of<object>();

        fixture.MapperMock.Setup((mapper) => mapper.TryMapParameter(parameter)).Returns(recorderMock.Object);

        var result = Target(fixture, parameter, argumentData);

        Assert.Equal(recorderReturnValue, result);

        recorderMock.Verify((recorder) => recorder.TryRecordData(fixture.DataRecordMock.Object, argumentData), Times.Once());
        recorderMock.VerifyNoOtherCalls();
    }
}
