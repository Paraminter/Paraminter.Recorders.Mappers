namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases.RecorderCases.ConstructorRecorderCases.NormalConstructorRecorderCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders;
using SharpAttributeParser.SyntacticRecorderComponents.SyntacticConstructorRecorderComponents;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(ISyntacticNormalConstructorRecorder recorder, IParameterSymbol parameter, ExpressionSyntax syntax) => recorder.TryRecordArgument(parameter, syntax);

    private readonly RecorderContext Context = RecorderContext.Create();

    [Fact]
    public void NullParameter_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Recorder, null!, ExpressionSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullSyntax_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Recorder, Mock.Of<IParameterSymbol>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullReturningMapper_ReturnsFalseAndLogs()
    {
        var parameter = Mock.Of<IParameterSymbol>();

        Context.MapperMock.Setup(static (mapper) => mapper.Constructor.TryMapParameter(It.IsAny<IParameterSymbol>())).Returns((ISyntacticMappedConstructorRecorder?)null);

        var outcome = Target(Context.Recorder, parameter, ExpressionSyntaxFactory.Create());

        Assert.False(outcome);

        Context.MapperMock.Verify((mapper) => mapper.Constructor.TryMapParameter(parameter), Times.Once);

        Context.LoggerFactoryMock.Verify((factory) => factory.Create<ISyntacticRecorder>().ConstructorArgument.FailedToMapConstructorParameterToRecorder(), Times.Once);
    }

    [Fact]
    public void TrueReturningRecorder_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void FalseReturningRecorder_ReturnsFalse() => ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private void ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var parameter = Mock.Of<IParameterSymbol>();
        var syntax = ExpressionSyntaxFactory.Create();

        Context.MapperMock.Setup(static (mapper) => mapper.Constructor.TryMapParameter(It.IsAny<IParameterSymbol>())!.Normal.TryRecordArgument(It.IsAny<ExpressionSyntax>())).Returns(recorderReturnValue);

        var outcome = Target(Context.Recorder, parameter, syntax);

        Assert.Equal(recorderReturnValue, outcome);

        Context.MapperMock.Verify((mapper) => mapper.Constructor.TryMapParameter(parameter)!.Normal.TryRecordArgument(syntax), Times.Once);
    }
}
