namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases.RecorderCases.ConstructorRecorderCases.NormalConstructorRecorderCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;
using SharpAttributeParser.SyntacticRecorderComponents.SyntacticConstructorRecorderComponents;

internal sealed class RecorderContext
{
    public static RecorderContext Create()
    {
        Mock<ISyntacticRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        SyntacticRecorderFactory factory = new(loggerFactoryMock.Object);

        Mock<ISyntacticMapper> mapperMock = new();

        var recorder = ((ISyntacticRecorderFactory)factory).Create(mapperMock.Object).Constructor.Normal;

        return new(recorder, mapperMock, loggerFactoryMock);
    }

    public ISyntacticNormalConstructorRecorder Recorder { get; }

    public Mock<ISyntacticMapper> MapperMock { get; }

    public Mock<ISyntacticRecorderLoggerFactory> LoggerFactoryMock { get; }

    private RecorderContext(ISyntacticNormalConstructorRecorder recorder, Mock<ISyntacticMapper> mapperMock, Mock<ISyntacticRecorderLoggerFactory> loggerFactoryMock)
    {
        Recorder = recorder;

        MapperMock = mapperMock;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
