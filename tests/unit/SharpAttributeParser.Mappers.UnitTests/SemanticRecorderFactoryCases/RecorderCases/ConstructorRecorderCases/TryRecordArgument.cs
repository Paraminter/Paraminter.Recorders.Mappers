namespace SharpAttributeParser.Mappers.SemanticRecorderFactoryCases.RecorderCases.ConstructorRecorderCases;

using Microsoft.CodeAnalysis;

using Moq;

using SharpAttributeParser.Mappers.SemanticMappedRecorders;
using SharpAttributeParser.SemanticRecorderComponents;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(ISemanticConstructorRecorder recorder, IParameterSymbol parameter, object? argument) => recorder.TryRecordArgument(parameter, argument);

    private readonly RecorderContext Context = RecorderContext.Create();

    [Fact]
    public void NullParameter_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Recorder, null!, Mock.Of<ITypeSymbol>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullReturningMapper_ReturnsFalseAndLogs()
    {
        var parameter = Mock.Of<IParameterSymbol>();

        Context.MapperMock.Setup(static (mapper) => mapper.Constructor.TryMapParameter(It.IsAny<IParameterSymbol>())).Returns((ISemanticMappedConstructorRecorder?)null);

        var outcome = Target(Context.Recorder, parameter, Mock.Of<object>());

        Assert.False(outcome);

        Context.MapperMock.Verify((mapper) => mapper.Constructor.TryMapParameter(parameter), Times.Once);

        Context.LoggerFactoryMock.Verify((factory) => factory.Create<ISemanticRecorder>().ConstructorArgument.FailedToMapConstructorParameterToRecorder(), Times.Once);
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

        Context.MapperMock.Setup(static (mapper) => mapper.Constructor.TryMapParameter(It.IsAny<IParameterSymbol>())!.TryRecordArgument(It.IsAny<object?>())).Returns(recorderReturnValue);

        var outcome = Target(Context.Recorder, parameter, argument);

        Assert.Equal(recorderReturnValue, outcome);

        Context.MapperMock.Verify((mapper) => mapper.Constructor.TryMapParameter(parameter)!.TryRecordArgument(argument), Times.Once);
    }
}
