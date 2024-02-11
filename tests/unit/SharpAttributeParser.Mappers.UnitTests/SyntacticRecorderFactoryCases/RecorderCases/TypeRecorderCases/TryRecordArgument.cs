namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases.RecorderCases.TypeRecorderCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders;
using SharpAttributeParser.SyntacticRecorderComponents;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(ISyntacticTypeRecorder recorder, ITypeParameterSymbol parameter, ExpressionSyntax syntax) => recorder.TryRecordArgument(parameter, syntax);

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
        var exception = Record.Exception(() => Target(Context.Recorder, Mock.Of<ITypeParameterSymbol>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullReturningMapper_ReturnsFalseAndLogs()
    {
        var parameter = Mock.Of<ITypeParameterSymbol>();

        Context.MapperMock.Setup(static (mapper) => mapper.Type.TryMapParameter(It.IsAny<ITypeParameterSymbol>())).Returns((ISyntacticMappedTypeRecorder?)null);

        var outcome = Target(Context.Recorder, parameter, ExpressionSyntaxFactory.Create());

        Assert.False(outcome);

        Context.MapperMock.Verify((mapper) => mapper.Type.TryMapParameter(parameter), Times.Once);

        Context.LoggerFactoryMock.Verify((factory) => factory.Create<ISyntacticRecorder>().TypeArgument.FailedToMapTypeParameterToRecorder(), Times.Once);
    }

    [Fact]
    public void TrueReturningRecorder_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void FalseReturningRecorder_ReturnsFalse() => ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private void ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var parameter = Mock.Of<ITypeParameterSymbol>();
        var syntax = ExpressionSyntaxFactory.Create();

        Context.MapperMock.Setup(static (mapper) => mapper.Type.TryMapParameter(It.IsAny<ITypeParameterSymbol>())!.TryRecordArgument(It.IsAny<ExpressionSyntax>())).Returns(recorderReturnValue);

        var outcome = Target(Context.Recorder, parameter, syntax);

        Assert.Equal(recorderReturnValue, outcome);

        Context.MapperMock.Verify((mapper) => mapper.Type.TryMapParameter(parameter)!.TryRecordArgument(syntax), Times.Once);
    }
}
