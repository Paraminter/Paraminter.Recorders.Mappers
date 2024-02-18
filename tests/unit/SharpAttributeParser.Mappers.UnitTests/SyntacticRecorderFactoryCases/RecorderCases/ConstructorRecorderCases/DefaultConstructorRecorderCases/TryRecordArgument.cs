namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases.RecorderCases.ConstructorRecorderCases.DefaultConstructorRecorderCases;

using Microsoft.CodeAnalysis;

using Moq;

using SharpAttributeParser.SyntacticRecorderComponents.SyntacticConstructorRecorderComponents;

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
    public void TrueReturningRecorder_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void FalseReturningRecorder_ReturnsFalse() => ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private static void ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var context = RecorderContext<object>.Create();

        var parameter = Mock.Of<IParameterSymbol>();

        context.MapperMock.Setup(static (mapper) => mapper.Constructor.Default.MapParameter(It.IsAny<IParameterSymbol>()).TryRecordArgument(It.IsAny<object>())).Returns(recorderReturnValue);

        var outcome = Target(context.Recorder, parameter);

        Assert.Equal(recorderReturnValue, outcome);

        context.MapperMock.Verify((mapper) => mapper.Constructor.Default.MapParameter(parameter).TryRecordArgument(context.DataRecord), Times.Once);
    }
}
