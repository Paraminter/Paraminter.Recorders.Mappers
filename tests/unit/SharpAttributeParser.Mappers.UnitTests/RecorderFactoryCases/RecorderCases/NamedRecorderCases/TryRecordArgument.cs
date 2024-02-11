namespace SharpAttributeParser.Mappers.RecorderFactoryCases.RecorderCases.NamedRecorderCases;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpAttributeParser.Mappers.MappedRecorders;
using SharpAttributeParser.RecorderComponents;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(INamedRecorder recorder, string parameterName, object? argument, ExpressionSyntax syntax) => recorder.TryRecordArgument(parameterName, argument, syntax);

    private readonly RecorderContext Context = RecorderContext.Create();

    [Fact]
    public void NullParameterName_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Recorder, null!, Mock.Of<object>(), ExpressionSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullSyntax_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Recorder, string.Empty, Mock.Of<object>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullReturningMapper_ReturnsFalseAndLogs()
    {
        var parameterName = string.Empty;

        Context.MapperMock.Setup(static (mapper) => mapper.Named.TryMapParameter(It.IsAny<string>())).Returns((IMappedNamedRecorder?)null);

        var outcome = Target(Context.Recorder, parameterName, Mock.Of<object>(), ExpressionSyntaxFactory.Create());

        Assert.False(outcome);

        Context.MapperMock.Verify((mapper) => mapper.Named.TryMapParameter(parameterName), Times.Once);

        Context.LoggerFactoryMock.Verify((factory) => factory.Create<IRecorder>().NamedArgument.FailedToMapNamedParameterToRecorder(), Times.Once);
    }

    [Fact]
    public void TrueReturningRecorder_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void FalseReturningRecorder_ReturnsFalse() => ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private void ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var parameterName = string.Empty;
        var argument = Mock.Of<object>();
        var syntax = ExpressionSyntaxFactory.Create();

        Context.MapperMock.Setup(static (mapper) => mapper.Named.TryMapParameter(It.IsAny<string>())!.TryRecordArgument(It.IsAny<object?>(), It.IsAny<ExpressionSyntax>())).Returns(recorderReturnValue);

        var outcome = Target(Context.Recorder, parameterName, argument, syntax);

        Assert.Equal(recorderReturnValue, outcome);

        Context.MapperMock.Verify((mapper) => mapper.Named.TryMapParameter(parameterName)!.TryRecordArgument(argument, syntax), Times.Once);
    }
}
