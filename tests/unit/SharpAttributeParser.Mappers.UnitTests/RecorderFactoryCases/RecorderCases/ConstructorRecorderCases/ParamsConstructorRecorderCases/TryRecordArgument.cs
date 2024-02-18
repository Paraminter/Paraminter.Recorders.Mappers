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

    [Fact]
    public void NullParameter_ArgumentNullException()
    {
        var context = RecorderContext<object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, null!, Mock.Of<object>(), Mock.Of<IReadOnlyList<ExpressionSyntax>>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullElementSyntax_ArgumentNullException()
    {
        var context = RecorderContext<object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, Mock.Of<IParameterSymbol>(), Mock.Of<object>(), null!));

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

        var parameter = Mock.Of<IParameterSymbol>();
        var argument = Mock.Of<object>();
        var elementSyntax = Mock.Of<IReadOnlyList<ExpressionSyntax>>();

        context.MapperMock.Setup(static (mapper) => mapper.Constructor.Params.MapParameter(It.IsAny<IParameterSymbol>()).TryRecordArgument(It.IsAny<object>(), It.IsAny<object?>(), It.IsAny<IReadOnlyList<ExpressionSyntax>>())).Returns(recorderReturnValue);

        var outcome = Target(context.Recorder, parameter, argument, elementSyntax);

        Assert.Equal(recorderReturnValue, outcome);

        context.MapperMock.Verify((mapper) => mapper.Constructor.Params.MapParameter(parameter).TryRecordArgument(context.DataRecord, argument, elementSyntax), Times.Once);
    }
}
