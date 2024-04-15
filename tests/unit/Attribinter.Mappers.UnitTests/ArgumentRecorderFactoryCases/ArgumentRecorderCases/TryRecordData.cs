namespace Attribinter.Mappers.ArgumentRecorderFactoryCases.ArgumentRecorderCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;

using Xunit;

public sealed class TryRecordData
{
    private static bool Target<TParameter, TRecord, TData>(IRecorderFixture<TParameter, TRecord, TData> fixture, TParameter parameter, TData data) where TRecord : class => fixture.Sut.TryRecordData(parameter, data);

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

        fixture.MapperMock.Setup((mapper) => mapper.TryMapParameter(parameter)).Returns((IMappedArgumentRecorder<object, object>?)null);

        var result = Target(fixture, parameter, data);

        Assert.False(result);
    }

    [Fact]
    public void NonNullReturningMapper_TrueReturningRecorder_ReturnsTrue() => NonNullReturningMapper_ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void NonNullReturningMapper_FalseReturningRecorder_ReturnsFalse() => NonNullReturningMapper_ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private static void NonNullReturningMapper_ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var fixture = RecorderFixtureFactory.Create<object, object, object>();

        Mock<IMappedArgumentRecorder<object, object>> recorderMock = new();

        recorderMock.Setup(static (recorder) => recorder.TryRecordData(It.IsAny<object>(), It.IsAny<object>())).Returns(recorderReturnValue);

        var parameter = Mock.Of<object>();
        var data = Mock.Of<object>();

        fixture.MapperMock.Setup(static (mapper) => mapper.TryMapParameter(It.IsAny<object>())).Returns(recorderMock.Object);

        var result = Target(fixture, parameter, data);

        Assert.Equal(recorderReturnValue, result);

        recorderMock.Verify((recorder) => recorder.TryRecordData(fixture.DataRecordMock.Object, data), Times.Once());
        recorderMock.VerifyNoOtherCalls();
    }
}
