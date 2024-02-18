namespace SharpAttributeParser.Mappers.RecorderFactoryCases.RecorderCases.TypeRecorderCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpAttributeParser.RecorderComponents;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(ITypeRecorder recorder, ITypeParameterSymbol parameter, ITypeSymbol argument, ExpressionSyntax syntax) => recorder.TryRecordArgument(parameter, argument, syntax);

    [Fact]
    public void NullParameter_ArgumentNullException()
    {
        var context = RecorderContext<object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, null!, Mock.Of<ITypeSymbol>(), ExpressionSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullArgument_ArgumentNullException()
    {
        var context = RecorderContext<object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, Mock.Of<ITypeParameterSymbol>(), null!, ExpressionSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullSyntax_ArgumentNullException()
    {
        var context = RecorderContext<object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, Mock.Of<ITypeParameterSymbol>(), Mock.Of<ITypeSymbol>(), null!));

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

        var parameter = Mock.Of<ITypeParameterSymbol>();
        var argument = Mock.Of<ITypeSymbol>();
        var syntax = ExpressionSyntaxFactory.Create();

        context.MapperMock.Setup(static (mapper) => mapper.Type.MapParameter(It.IsAny<ITypeParameterSymbol>()).TryRecordArgument(It.IsAny<object>(), It.IsAny<ITypeSymbol>(), It.IsAny<ExpressionSyntax>())).Returns(recorderReturnValue);

        var outcome = Target(context.Recorder, parameter, argument, syntax);

        Assert.Equal(recorderReturnValue, outcome);

        context.MapperMock.Verify((mapper) => mapper.Type.MapParameter(parameter).TryRecordArgument(context.DataRecord, argument, syntax), Times.Once);
    }
}
