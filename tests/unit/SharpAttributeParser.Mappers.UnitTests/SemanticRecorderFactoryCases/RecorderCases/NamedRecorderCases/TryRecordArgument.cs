namespace SharpAttributeParser.Mappers.SemanticRecorderFactoryCases.RecorderCases.NamedRecorderCases;

using Microsoft.CodeAnalysis;

using Moq;

using SharpAttributeParser.Mappers.SemanticMappedRecorders;
using SharpAttributeParser.SemanticRecorderComponents;

using System;

using Xunit;

public sealed class TryRecordArgument
{
    private static bool Target(ISemanticNamedRecorder recorder, string parameterName, object? argument) => recorder.TryRecordArgument(parameterName, argument);

    private readonly RecorderContext Context = RecorderContext.Create();

    [Fact]
    public void NullParameterName_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Recorder, null!, Mock.Of<ITypeSymbol>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullReturningMapper_ReturnsFalseAndLogs()
    {
        var parameterName = string.Empty;

        Context.MapperMock.Setup(static (mapper) => mapper.Named.TryMapParameter(It.IsAny<string>())).Returns((ISemanticMappedNamedRecorder?)null);

        var outcome = Target(Context.Recorder, parameterName, Mock.Of<object>());

        Assert.False(outcome);

        Context.MapperMock.Verify((mapper) => mapper.Named.TryMapParameter(parameterName), Times.Once);

        Context.LoggerFactoryMock.Verify((factory) => factory.Create<ISemanticRecorder>().NamedArgument.FailedToMapNamedParameterToRecorder(), Times.Once);
    }

    [Fact]
    public void TrueReturningRecorder_ReturnsTrue() => ValidRecorder_PropagatesReturnValue(true);

    [Fact]
    public void FalseReturningRecorder_ReturnsFalse() => ValidRecorder_PropagatesReturnValue(false);

    [AssertionMethod]
    private void ValidRecorder_PropagatesReturnValue(bool recorderReturnValue)
    {
        var parameterName = string.Empty;
        var argument = Mock.Of<object>();

        Context.MapperMock.Setup(static (mapper) => mapper.Named.TryMapParameter(It.IsAny<string>())!.TryRecordArgument(It.IsAny<object?>())).Returns(recorderReturnValue);

        var outcome = Target(Context.Recorder, parameterName, argument);

        Assert.Equal(recorderReturnValue, outcome);

        Context.MapperMock.Verify((mapper) => mapper.Named.TryMapParameter(parameterName)!.TryRecordArgument(argument), Times.Once);
    }
}
