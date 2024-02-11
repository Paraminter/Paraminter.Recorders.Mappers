namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases.RecorderCases.TypeRecorderCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;
using SharpAttributeParser.SyntacticRecorderComponents;

internal sealed class RecorderContext
{
    public static RecorderContext Create()
    {
        Mock<ISyntacticRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        SyntacticRecorderFactory factory = new(loggerFactoryMock.Object);

        Mock<ISyntacticMapper> mapperMock = new();

        var recorder = ((ISyntacticRecorderFactory)factory).Create(mapperMock.Object).Type;

        return new(recorder, mapperMock, loggerFactoryMock);
    }

    public ISyntacticTypeRecorder Recorder { get; }

    public Mock<ISyntacticMapper> MapperMock { get; }

    public Mock<ISyntacticRecorderLoggerFactory> LoggerFactoryMock { get; }

    private RecorderContext(ISyntacticTypeRecorder recorder, Mock<ISyntacticMapper> mapperMock, Mock<ISyntacticRecorderLoggerFactory> loggerFactoryMock)
    {
        Recorder = recorder;

        MapperMock = mapperMock;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
