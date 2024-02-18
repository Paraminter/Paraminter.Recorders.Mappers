namespace SharpAttributeParser.Mappers.RecorderFactoryCases.RecorderCases.NamedRecorderCases;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpAttributeParser.RecorderComponents;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(INamedRecorder recorder, string parameterName, object? argument, ExpressionSyntax syntax) => recorder.TryRecordArgument(parameterName, argument, syntax);

    [Fact]
    public void NullParameterName_ArgumentNullException()
    {
        var context = RecorderContext<object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, null!, Mock.Of<object>(), ExpressionSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullSyntax_ArgumentNullException()
    {
        var context = RecorderContext<object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, string.Empty, Mock.Of<object>(), null!));

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
        var syntax = ExpressionSyntaxFactory.Create();

        context.MapperMock.Setup(static (mapper) => mapper.Named.MapParameter(It.IsAny<string>()).TryRecordArgument(It.IsAny<object>(), It.IsAny<object?>(), It.IsAny<ExpressionSyntax>())).Returns(recorderReturnValue);

        var outcome = Target(context.Recorder, parameterName, argument, syntax);

        Assert.Equal(recorderReturnValue, outcome);

        context.MapperMock.Verify((mapper) => mapper.Named.MapParameter(parameterName).TryRecordArgument(context.DataRecord, argument, syntax), Times.Once);
    }
}
