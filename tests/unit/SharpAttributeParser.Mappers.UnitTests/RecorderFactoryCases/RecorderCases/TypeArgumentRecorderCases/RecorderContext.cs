namespace SharpAttributeParser.Mappers.RecorderFactoryCases.RecorderCases.TypeRecorderCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;
using SharpAttributeParser.RecorderComponents;

internal sealed class RecorderContext<TRecord> where TRecord : class
{
    public static RecorderContext<TRecord> Create()
    {
        Mock<IRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        RecorderFactory factory = new(loggerFactoryMock.Object);

        Mock<IMapper<TRecord>> mapperMock = new();
        var dataRecord = Mock.Of<TRecord>();

        var recorder = ((IRecorderFactory)factory).Create(mapperMock.Object, dataRecord).Type;

        return new(recorder, mapperMock, dataRecord, loggerFactoryMock);
    }

    public ITypeRecorder Recorder { get; }

    public Mock<IMapper<TRecord>> MapperMock { get; }
    public TRecord DataRecord { get; }

    public Mock<IRecorderLoggerFactory> LoggerFactoryMock { get; }

    private RecorderContext(ITypeRecorder recorder, Mock<IMapper<TRecord>> mapperMock, TRecord dataRecord, Mock<IRecorderLoggerFactory> loggerFactoryMock)
    {
        Recorder = recorder;

        MapperMock = mapperMock;
        DataRecord = dataRecord;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
