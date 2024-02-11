namespace SharpAttributeParser.Mappers.RecorderFactoryCases.RecorderCases.TypeRecorderCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;
using SharpAttributeParser.RecorderComponents;

internal sealed class RecorderContext
{
    public static RecorderContext Create()
    {
        Mock<IRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        RecorderFactory factory = new(loggerFactoryMock.Object);

        Mock<IMapper> mapperMock = new();

        var recorder = ((IRecorderFactory)factory).Create(mapperMock.Object).Type;

        return new(recorder, mapperMock, loggerFactoryMock);
    }

    public ITypeRecorder Recorder { get; }

    public Mock<IMapper> MapperMock { get; }

    public Mock<IRecorderLoggerFactory> LoggerFactoryMock { get; }

    private RecorderContext(ITypeRecorder recorder, Mock<IMapper> mapperMock, Mock<IRecorderLoggerFactory> loggerFactoryMock)
    {
        Recorder = recorder;

        MapperMock = mapperMock;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
