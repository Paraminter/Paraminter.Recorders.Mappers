namespace SharpAttributeParser.Mappers.SemanticRecorderFactoryCases.RecorderCases.TypeRecorderCases;

using Microsoft.CodeAnalysis;

using Moq;

using SharpAttributeParser.SemanticRecorderComponents;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(ISemanticTypeRecorder recorder, ITypeParameterSymbol parameter, ITypeSymbol argument) => recorder.TryRecordArgument(parameter, argument);

    private readonly RecorderContext Context = RecorderContext.Create();

    [Fact]
    public void NullParameter_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Recorder, null!, Mock.Of<ITypeSymbol>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullArgument_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Recorder, Mock.Of<ITypeParameterSymbol>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void TrueReturningRecorder_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void FalseReturningRecorder_ReturnsFalse() => ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private void ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var parameter = Mock.Of<ITypeParameterSymbol>();
        var argument = Mock.Of<ITypeSymbol>();

        Context.MapperMock.Setup(static (mapper) => mapper.Type.MapParameter(It.IsAny<ITypeParameterSymbol>()).TryRecordArgument(It.IsAny<ITypeSymbol>())).Returns(recorderReturnValue);

        var outcome = Target(Context.Recorder, parameter, argument);

        Assert.Equal(recorderReturnValue, outcome);

        Context.MapperMock.Verify((mapper) => mapper.Type.MapParameter(parameter).TryRecordArgument(argument), Times.Once);
    }
}
