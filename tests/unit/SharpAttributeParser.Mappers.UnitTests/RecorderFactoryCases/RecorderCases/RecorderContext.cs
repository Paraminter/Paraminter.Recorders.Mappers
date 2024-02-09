namespace SharpAttributeParser.Mappers.RecorderFactoryCases.RecorderCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;

internal sealed class RecorderContext<TRecord> where TRecord : class
{
    public static RecorderContext<TRecord> Create()
    {
        Mock<IRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        RecorderFactory factory = new(loggerFactoryMock.Object);

        Mock<IMapper<TRecord>> mapperMock = new();
        Mock<TRecord> dataRecordMock = new();

        var recorder = ((IRecorderFactory)factory).Create(mapperMock.Object, dataRecordMock.Object);

        return new(recorder, mapperMock, dataRecordMock, loggerFactoryMock);
    }

    public IRecorder<TRecord> Recorder { get; }

    public Mock<IMapper<TRecord>> MapperMock { get; }
    public Mock<TRecord> DataRecordMock { get; }

    public Mock<IRecorderLoggerFactory> LoggerFactoryMock { get; }

    private RecorderContext(IRecorder<TRecord> recorder, Mock<IMapper<TRecord>> mapperMock, Mock<TRecord> dataRecordMock, Mock<IRecorderLoggerFactory> loggerFactoryMock)
    {
        Recorder = recorder;

        MapperMock = mapperMock;
        DataRecordMock = dataRecordMock;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
