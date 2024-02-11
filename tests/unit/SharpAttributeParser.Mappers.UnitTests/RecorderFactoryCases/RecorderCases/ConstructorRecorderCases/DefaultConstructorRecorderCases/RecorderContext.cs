namespace SharpAttributeParser.Mappers.RecorderFactoryCases.RecorderCases.ConstructorRecorderCases.DefaultConstructorRecorderCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;
using SharpAttributeParser.RecorderComponents.ConstructorRecorderComponents;

internal sealed class RecorderContext
{
    public static RecorderContext Create()
    {
        Mock<IRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        RecorderFactory factory = new(loggerFactoryMock.Object);

        Mock<IMapper> mapperMock = new();

        var recorder = ((IRecorderFactory)factory).Create(mapperMock.Object).Constructor.Default;

        return new(recorder, mapperMock, loggerFactoryMock);
    }

    public IDefaultConstructorRecorder Recorder { get; }

    public Mock<IMapper> MapperMock { get; }

    public Mock<IRecorderLoggerFactory> LoggerFactoryMock { get; }

    private RecorderContext(IDefaultConstructorRecorder recorder, Mock<IMapper> mapperMock, Mock<IRecorderLoggerFactory> loggerFactoryMock)
    {
        Recorder = recorder;

        MapperMock = mapperMock;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
