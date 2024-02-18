namespace SharpAttributeParser.Mappers.RecorderFactoryCases.RecorderCases.ConstructorRecorderCases.ParamsConstructorRecorderCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpAttributeParser.RecorderComponents.ConstructorRecorderComponents;

using System;
using System.Collections.Generic;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(IParamsConstructorRecorder recorder, IParameterSymbol parameter, object? argument, IReadOnlyList<ExpressionSyntax> elementSyntax) => recorder.TryRecordArgument(parameter, argument, elementSyntax);

    private readonly RecorderContext Context = RecorderContext.Create();

    [Fact]
    public void NullParameter_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Recorder, null!, Mock.Of<object>(), Mock.Of<IReadOnlyList<ExpressionSyntax>>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullElementSyntax_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Recorder, Mock.Of<IParameterSymbol>(), Mock.Of<object>(), null!));

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
        var argument = Mock.Of<object>();
        var elementSyntax = Mock.Of<IReadOnlyList<ExpressionSyntax>>();

        Context.MapperMock.Setup(static (mapper) => mapper.Constructor.Params.MapParameter(It.IsAny<IParameterSymbol>()).TryRecordArgument(It.IsAny<object?>(), It.IsAny<IReadOnlyList<ExpressionSyntax>>())).Returns(recorderReturnValue);

        var outcome = Target(Context.Recorder, parameter, argument, elementSyntax);

        Assert.Equal(recorderReturnValue, outcome);

        Context.MapperMock.Verify((mapper) => mapper.Constructor.Params.MapParameter(parameter).TryRecordArgument(argument, elementSyntax), Times.Once);
    }
}
