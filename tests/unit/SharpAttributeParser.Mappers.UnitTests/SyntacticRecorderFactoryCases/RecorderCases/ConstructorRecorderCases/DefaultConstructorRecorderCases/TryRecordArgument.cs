namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases.RecorderCases.ConstructorRecorderCases.DefaultConstructorRecorderCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(ISyntacticDefaultConstructorRecorder recorder, IParameterSymbol parameter) => recorder.TryRecordArgument(parameter);

    [Fact]
    public void NullParameter_ArgumentNullException()
    {
        var context = RecorderContext<object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullReturningMapper_ReturnsFalseAndLogs()
    {
        var parameter = Mock.Of<IParameterSymbol>();

        var context = RecorderContext<object>.Create();

        context.MapperMock.Setup(static (mapper) => mapper.Constructor.TryMapParameter(It.IsAny<IParameterSymbol>(), It.IsAny<object>())).Returns((ISyntacticMappedConstructorRecorder?)null);

        var outcome = Target(context.Recorder, parameter);

        Assert.False(outcome);

        context.MapperMock.Verify((mapper) => mapper.Constructor.TryMapParameter(parameter, context.DataRecordMock.Object), Times.Once);

        context.LoggerFactoryMock.Verify((factory) => factory.Create<ISyntacticRecorder>().ConstructorArgument.FailedToMapConstructorParameterToRecorder(), Times.Once);
    }

    [Fact]
    public void TrueReturningRecorder_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void FalseReturningRecorder_ReturnsFalse() => ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private static void ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var parameter = Mock.Of<IParameterSymbol>();

        var context = RecorderContext<object>.Create();

        context.MapperMock.Setup(static (mapper) => mapper.Constructor.TryMapParameter(It.IsAny<IParameterSymbol>(), It.IsAny<object>())!.Default.TryRecordArgument()).Returns(recorderReturnValue);

        var outcome = Target(context.Recorder, parameter);

        Assert.Equal(recorderReturnValue, outcome);

        context.MapperMock.Verify((mapper) => mapper.Constructor.TryMapParameter(parameter, context.DataRecordMock.Object)!.Default.TryRecordArgument(), Times.Once);
    }
}
