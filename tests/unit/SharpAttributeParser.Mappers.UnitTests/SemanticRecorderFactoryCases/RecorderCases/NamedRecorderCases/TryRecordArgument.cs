namespace SharpAttributeParser.Mappers.SemanticRecorderFactoryCases.RecorderCases.NamedRecorderCases;

using Microsoft.CodeAnalysis;

using Moq;

using SharpAttributeParser.SemanticRecorderComponents;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(ISemanticNamedRecorder recorder, string parameterName, object? argument) => recorder.TryRecordArgument(parameterName, argument);

    [Fact]
    public void NullParameterName_ArgumentNullException()
    {
        var context = RecorderContext<object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, null!, Mock.Of<ITypeSymbol>()));

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

        var parameterName = string.Empty;
        var argument = Mock.Of<object>();

        context.MapperMock.Setup(static (mapper) => mapper.Named.MapParameter(It.IsAny<string>()).TryRecordArgument(It.IsAny<object>(), It.IsAny<object?>())).Returns(recorderReturnValue);

        var outcome = Target(context.Recorder, parameterName, argument);

        Assert.Equal(recorderReturnValue, outcome);

        context.MapperMock.Verify((mapper) => mapper.Named.MapParameter(parameterName).TryRecordArgument(context.DataRecord, argument), Times.Once);
    }
}
