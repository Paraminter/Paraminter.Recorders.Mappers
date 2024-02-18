namespace SharpAttributeParser.Mappers.SemanticRecorderFactoryCases.RecorderCases.TypeRecorderCases;

using Microsoft.CodeAnalysis;

using Moq;

using SharpAttributeParser.SemanticRecorderComponents;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(ISemanticTypeRecorder recorder, ITypeParameterSymbol parameter, ITypeSymbol argument) => recorder.TryRecordArgument(parameter, argument);

    [Fact]
    public void NullParameter_ArgumentNullException()
    {
        var context = RecorderContext<object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, null!, Mock.Of<ITypeSymbol>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullArgument_ArgumentNullException()
    {
        var context = RecorderContext<object>.Create();

        var exception = Record.Exception(() => Target(context.Recorder, Mock.Of<ITypeParameterSymbol>(), null!));

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

        context.MapperMock.Setup(static (mapper) => mapper.Type.MapParameter(It.IsAny<ITypeParameterSymbol>()).TryRecordArgument(It.IsAny<object>(), It.IsAny<ITypeSymbol>())).Returns(recorderReturnValue);

        var outcome = Target(context.Recorder, parameter, argument);

        Assert.Equal(recorderReturnValue, outcome);

        context.MapperMock.Verify((mapper) => mapper.Type.MapParameter(parameter).TryRecordArgument(context.DataRecord, argument), Times.Once);
    }
}
