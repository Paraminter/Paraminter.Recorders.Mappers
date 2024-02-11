namespace SharpAttributeParser.Mappers.SemanticRecorderFactoryCases.RecorderCases.TypeRecorderCases;

using Microsoft.CodeAnalysis;

using Moq;

using SharpAttributeParser.Mappers.SemanticMappedRecorders;
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
    public void NullReturningMapper_ReturnsFalseAndLogs()
    {
        var parameter = Mock.Of<ITypeParameterSymbol>();

        Context.MapperMock.Setup(static (mapper) => mapper.Type.TryMapParameter(It.IsAny<ITypeParameterSymbol>())).Returns((ISemanticMappedTypeRecorder?)null);

        var outcome = Target(Context.Recorder, parameter, Mock.Of<ITypeSymbol>());

        Assert.False(outcome);

        Context.MapperMock.Verify((mapper) => mapper.Type.TryMapParameter(parameter), Times.Once);

        Context.LoggerFactoryMock.Verify((factory) => factory.Create<ISemanticRecorder>().TypeArgument.FailedToMapTypeParameterToRecorder(), Times.Once);
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

        Context.MapperMock.Setup(static (mapper) => mapper.Type.TryMapParameter(It.IsAny<ITypeParameterSymbol>())!.TryRecordArgument(It.IsAny<ITypeSymbol>())).Returns(recorderReturnValue);

        var outcome = Target(Context.Recorder, parameter, argument);

        Assert.Equal(recorderReturnValue, outcome);

        Context.MapperMock.Verify((mapper) => mapper.Type.TryMapParameter(parameter)!.TryRecordArgument(argument), Times.Once);
    }
}
