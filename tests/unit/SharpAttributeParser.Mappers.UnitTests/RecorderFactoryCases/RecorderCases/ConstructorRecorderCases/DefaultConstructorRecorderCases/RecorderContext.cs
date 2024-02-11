namespace SharpAttributeParser.Mappers.RecorderFactoryCases.RecorderCases.ConstructorRecorderCases.DefaultConstructorRecorderCases;

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

        var recorder = ((IRecorderFactory)factory).Create(mapperMock.Object, dataRecordMock.Object).Constructor.Default;

        return new(recorder, mapperMock, dataRecordMock, loggerFactoryMock);
    }

    public IDefaultConstructorRecorder Recorder { get; }

    public Mock<IMapper<TRecord>> MapperMock { get; }
    public Mock<TRecord> DataRecordMock { get; }

    public Mock<IRecorderLoggerFactory> LoggerFactoryMock { get; }

    private RecorderContext(IDefaultConstructorRecorder recorder, Mock<IMapper<TRecord>> mapperMock, Mock<TRecord> dataRecordMock, Mock<IRecorderLoggerFactory> loggerFactoryMock)
    {
        Recorder = recorder;

        MapperMock = mapperMock;
        DataRecordMock = dataRecordMock;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
