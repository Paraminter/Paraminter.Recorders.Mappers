namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases.RecorderCases.NamedRecorderCases;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders;
using SharpAttributeParser.SyntacticRecorderComponents;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(ISyntacticNamedRecorder recorder, string parameterName, ExpressionSyntax syntax) => recorder.TryRecordArgument(parameterName, syntax);

    private readonly RecorderContext Context = RecorderContext.Create();

    [Fact]
    public void NullParameterName_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Recorder, null!, ExpressionSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullSyntax_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Recorder, string.Empty, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullReturningMapper_ReturnsFalseAndLogs()
    {
        var parameterName = string.Empty;

        Context.MapperMock.Setup(static (mapper) => mapper.Named.TryMapParameter(It.IsAny<string>())).Returns((ISyntacticMappedNamedRecorder?)null);

        var outcome = Target(Context.Recorder, parameterName, ExpressionSyntaxFactory.Create());

        Assert.False(outcome);

        Context.MapperMock.Verify((mapper) => mapper.Named.TryMapParameter(parameterName), Times.Once);

        Context.LoggerFactoryMock.Verify((factory) => factory.Create<ISyntacticRecorder>().NamedArgument.FailedToMapNamedParameterToRecorder(), Times.Once);
    }

    [Fact]
    public void TrueReturningRecorder_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void FalseReturningRecorder_ReturnsFalse() => ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private void ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var parameterName = string.Empty;
        var syntax = ExpressionSyntaxFactory.Create();

        Context.MapperMock.Setup(static (mapper) => mapper.Named.TryMapParameter(It.IsAny<string>())!.TryRecordArgument(It.IsAny<ExpressionSyntax>())).Returns(recorderReturnValue);

        var outcome = Target(Context.Recorder, parameterName, syntax);

        Assert.Equal(recorderReturnValue, outcome);

        Context.MapperMock.Verify((mapper) => mapper.Named.TryMapParameter(parameterName)!.TryRecordArgument(syntax), Times.Once);
    }
}
