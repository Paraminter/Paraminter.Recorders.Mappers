namespace SharpAttributeParser.Mappers.SemanticRecorderFactoryCases.RecorderCases.NamedRecorderCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;
using SharpAttributeParser.SemanticRecorderComponents;

internal sealed class RecorderContext
{
    public static RecorderContext Create()
    {
        Mock<ISemanticRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        SemanticRecorderFactory factory = new(loggerFactoryMock.Object);

        Mock<ISemanticMapper> mapperMock = new();

        var recorder = ((ISemanticRecorderFactory)factory).Create(mapperMock.Object).Named;

        return new(recorder, mapperMock, loggerFactoryMock);
    }

    public ISemanticNamedRecorder Recorder { get; }

    public Mock<ISemanticMapper> MapperMock { get; }

    public Mock<ISemanticRecorderLoggerFactory> LoggerFactoryMock { get; }

    private RecorderContext(ISemanticNamedRecorder recorder, Mock<ISemanticMapper> mapperMock, Mock<ISemanticRecorderLoggerFactory> loggerFactoryMock)
    {
        Recorder = recorder;

        MapperMock = mapperMock;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
