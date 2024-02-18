namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases.RecorderCases.ConstructorRecorderCases.NormalConstructorRecorderCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

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
    public void TrueReturningRecorder_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void FalseReturningRecorder_ReturnsFalse() => ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private void ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var parameter = Mock.Of<IParameterSymbol>();
        var syntax = ExpressionSyntaxFactory.Create();

        Context.MapperMock.Setup(static (mapper) => mapper.Constructor.Normal.MapParameter(It.IsAny<IParameterSymbol>()).TryRecordArgument(It.IsAny<ExpressionSyntax>())).Returns(recorderReturnValue);

        var outcome = Target(Context.Recorder, parameter, syntax);

        Assert.Equal(recorderReturnValue, outcome);

        Context.MapperMock.Verify((mapper) => mapper.Constructor.Normal.MapParameter(parameter).TryRecordArgument(syntax), Times.Once);
    }
}
