namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases.RecorderCases.NamedRecorderCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;
using SharpAttributeParser.SyntacticRecorderComponents;

internal sealed class RecorderContext<TRecord> where TRecord : class
{
    public static RecorderContext<TRecord> Create()
    {
        Mock<ISyntacticRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        SyntacticRecorderFactory factory = new(loggerFactoryMock.Object);

        Mock<ISyntacticMapper<TRecord>> mapperMock = new();
        var dataRecord = Mock.Of<TRecord>();

        var recorder = ((ISyntacticRecorderFactory)factory).Create(mapperMock.Object, dataRecord).Named;

        return new(recorder, mapperMock, dataRecord, loggerFactoryMock);
    }

    public ISyntacticNamedRecorder Recorder { get; }

    public Mock<ISyntacticMapper<TRecord>> MapperMock { get; }
    public TRecord DataRecord { get; }

    public Mock<ISyntacticRecorderLoggerFactory> LoggerFactoryMock { get; }

    private RecorderContext(ISyntacticNamedRecorder recorder, Mock<ISyntacticMapper<TRecord>> mapperMock, TRecord dataRecord, Mock<ISyntacticRecorderLoggerFactory> loggerFactoryMock)
    {
        Recorder = recorder;

        MapperMock = mapperMock;
        DataRecord = dataRecord;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
