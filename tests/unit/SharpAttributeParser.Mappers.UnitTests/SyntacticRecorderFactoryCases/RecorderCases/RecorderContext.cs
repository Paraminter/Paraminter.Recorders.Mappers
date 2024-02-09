namespace SharpAttributeParser.Mappers.RecorderFactoryCases.SyntacticRecorderCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;

internal sealed class RecorderContext<TRecord> where TRecord : class
{
    public static RecorderContext<TRecord> Create()
    {
        Mock<ISyntacticRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        SyntacticRecorderFactory factory = new(loggerFactoryMock.Object);

        Mock<ISyntacticMapper<TRecord>> mapperMock = new();
        Mock<TRecord> dataRecordMock = new();

        var recorder = ((ISyntacticRecorderFactory)factory).Create(mapperMock.Object, dataRecordMock.Object);

        return new(recorder, mapperMock, dataRecordMock, loggerFactoryMock);
    }

    public ISyntacticRecorder<TRecord> Recorder { get; }

    public Mock<ISyntacticMapper<TRecord>> MapperMock { get; }
    public Mock<TRecord> DataRecordMock { get; }

    public Mock<ISyntacticRecorderLoggerFactory> LoggerFactoryMock { get; }

    private RecorderContext(ISyntacticRecorder<TRecord> recorder, Mock<ISyntacticMapper<TRecord>> mapperMock, Mock<TRecord> dataRecordMock, Mock<ISyntacticRecorderLoggerFactory> loggerFactoryMock)
    {
        Recorder = recorder;

        MapperMock = mapperMock;
        DataRecordMock = dataRecordMock;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
