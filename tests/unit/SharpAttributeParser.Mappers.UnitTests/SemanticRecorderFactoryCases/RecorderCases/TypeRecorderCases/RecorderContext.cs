namespace SharpAttributeParser.Mappers.SemanticRecorderFactoryCases.RecorderCases.TypeRecorderCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;
using SharpAttributeParser.SemanticRecorderComponents;

internal sealed class RecorderContext<TRecord> where TRecord : class
{
    public static RecorderContext<TRecord> Create()
    {
        Mock<ISemanticRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        SemanticRecorderFactory factory = new(loggerFactoryMock.Object);

        Mock<ISemanticMapper<TRecord>> mapperMock = new();
        var dataRecord = Mock.Of<TRecord>();

        var recorder = ((ISemanticRecorderFactory)factory).Create(mapperMock.Object, dataRecord).Type;

        return new(recorder, mapperMock, dataRecord, loggerFactoryMock);
    }

    public ISemanticTypeRecorder Recorder { get; }

    public Mock<ISemanticMapper<TRecord>> MapperMock { get; }
    public TRecord DataRecord { get; }

    public Mock<ISemanticRecorderLoggerFactory> LoggerFactoryMock { get; }

    private RecorderContext(ISemanticTypeRecorder recorder, Mock<ISemanticMapper<TRecord>> mapperMock, TRecord dataRecord, Mock<ISemanticRecorderLoggerFactory> loggerFactoryMock)
    {
        Recorder = recorder;

        MapperMock = mapperMock;
        DataRecord = dataRecord;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
