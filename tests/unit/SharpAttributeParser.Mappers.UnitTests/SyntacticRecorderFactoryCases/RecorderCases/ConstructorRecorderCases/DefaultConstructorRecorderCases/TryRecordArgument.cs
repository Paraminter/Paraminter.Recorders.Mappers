namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases.RecorderCases.ConstructorRecorderCases.DefaultConstructorRecorderCases;

using Microsoft.CodeAnalysis;

using Moq;

using SharpAttributeParser.SyntacticRecorderComponents.SyntacticConstructorRecorderComponents;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(ISyntacticDefaultConstructorRecorder recorder, IParameterSymbol parameter) => recorder.TryRecordArgument(parameter);

    private readonly RecorderContext Context = RecorderContext.Create();

    [Fact]
    public void NullParameter_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Recorder, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void TrueReturningRecorder_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void FalseReturningRecorder_ReturnsFalse() => ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private void ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var parameter = Mock.Of<IParameterSymbol>();

        Context.MapperMock.Setup(static (mapper) => mapper.Constructor.Default.MapParameter(It.IsAny<IParameterSymbol>()).TryRecordArgument()).Returns(recorderReturnValue);

        var outcome = Target(Context.Recorder, parameter);

        Assert.Equal(recorderReturnValue, outcome);

        Context.MapperMock.Verify((mapper) => mapper.Constructor.Default.MapParameter(parameter).TryRecordArgument(), Times.Once);
    }
}
