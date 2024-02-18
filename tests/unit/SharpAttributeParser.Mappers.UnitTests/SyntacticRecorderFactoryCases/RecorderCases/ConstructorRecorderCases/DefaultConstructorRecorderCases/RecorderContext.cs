namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases.RecorderCases.ConstructorRecorderCases.DefaultConstructorRecorderCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;
using SharpAttributeParser.SyntacticRecorderComponents.SyntacticConstructorRecorderComponents;

internal sealed class RecorderContext<TRecord> where TRecord : class
{
    public static RecorderContext<TRecord> Create()
    {
        Mock<ISyntacticRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        SyntacticRecorderFactory factory = new(loggerFactoryMock.Object);

        Mock<ISyntacticMapper<TRecord>> mapperMock = new();
        var dataRecord = Mock.Of<TRecord>();

        var recorder = ((ISyntacticRecorderFactory)factory).Create(mapperMock.Object, dataRecord).Constructor.Default;

        return new(recorder, mapperMock, dataRecord, loggerFactoryMock);
    }

    public ISyntacticDefaultConstructorRecorder Recorder { get; }

    public Mock<ISyntacticMapper<TRecord>> MapperMock { get; }
    public TRecord DataRecord { get; }

    public Mock<ISyntacticRecorderLoggerFactory> LoggerFactoryMock { get; }

    private RecorderContext(ISyntacticDefaultConstructorRecorder recorder, Mock<ISyntacticMapper<TRecord>> mapperMock, TRecord dataRecord, Mock<ISyntacticRecorderLoggerFactory> loggerFactoryMock)
    {
        Recorder = recorder;

        MapperMock = mapperMock;
        DataRecord = dataRecord;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
