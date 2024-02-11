namespace SharpAttributeParser.Mappers.RecorderFactoryCases.RecorderCases.ConstructorRecorderCases.NormalConstructorRecorderCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(INormalConstructorRecorder recorder, IParameterSymbol parameter, object? argument, ExpressionSyntax syntax) => recorder.TryRecordArgument(parameter, argument, syntax);

    [Fact]
    public void NullParameter_ArgumentNullException()
    {
        var context = RecorderContext<object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, null!, Mock.Of<object>(), ExpressionSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullSyntax_ArgumentNullException()
    {
        var context = RecorderContext<object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, Mock.Of<IParameterSymbol>(), Mock.Of<object>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullReturningMapper_ReturnsFalseAndLogs()
    {
        var parameter = Mock.Of<IParameterSymbol>();

        var context = RecorderContext<object>.Create();

        context.MapperMock.Setup(static (mapper) => mapper.Constructor.TryMapParameter(It.IsAny<IParameterSymbol>(), It.IsAny<object>())).Returns((IMappedConstructorRecorder?)null);

        var outcome = Target(context.Recorder, parameter, Mock.Of<object>(), ExpressionSyntaxFactory.Create());

        Assert.False(outcome);

        context.MapperMock.Verify((mapper) => mapper.Constructor.TryMapParameter(parameter, context.DataRecordMock.Object), Times.Once);

        context.LoggerFactoryMock.Verify((factory) => factory.Create<IRecorder>().ConstructorArgument.FailedToMapConstructorParameterToRecorder(), Times.Once);
    }

    [Fact]
    public void TrueReturningRecorder_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void FalseReturningRecorder_ReturnsFalse() => ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private static void ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var parameter = Mock.Of<IParameterSymbol>();
        var argument = Mock.Of<object>();
        var syntax = ExpressionSyntaxFactory.Create();

        var context = RecorderContext<object>.Create();

        context.MapperMock.Setup(static (mapper) => mapper.Constructor.TryMapParameter(It.IsAny<IParameterSymbol>(), It.IsAny<object>())!.Normal.TryRecordArgument(It.IsAny<object?>(), It.IsAny<ExpressionSyntax>())).Returns(recorderReturnValue);

        var outcome = Target(context.Recorder, parameter, argument, syntax);

        Assert.Equal(recorderReturnValue, outcome);

        context.MapperMock.Verify((mapper) => mapper.Constructor.TryMapParameter(parameter, context.DataRecordMock.Object)!.Normal.TryRecordArgument(argument, syntax), Times.Once);
    }
}
