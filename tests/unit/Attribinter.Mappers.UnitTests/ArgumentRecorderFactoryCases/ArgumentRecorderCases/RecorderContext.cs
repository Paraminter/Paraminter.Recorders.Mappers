namespace Attribinter.Mappers.ArgumentRecorderFactoryCases.ArgumentRecorderCases;

using Moq;

internal sealed class RecorderContext<TParameter, TRecord, TData> where TRecord : class
{
    public static RecorderContext<TParameter, TRecord, TData> Create()
    {
        ArgumentRecorderFactory factory = new();

        Mock<IParameterMapper<TParameter, TRecord, TData>> mapperMock = new();
        var dataRecord = Mock.Of<TRecord>();

        var recorder = ((IArgumentRecorderFactory)factory).Create(mapperMock.Object, dataRecord);

        return new(recorder, mapperMock, dataRecord);
    }

    public IArgumentRecorder<TParameter, TData> Recorder { get; }

    public Mock<IParameterMapper<TParameter, TRecord, TData>> MapperMock { get; }
    public TRecord DataRecord { get; }

    private RecorderContext(IArgumentRecorder<TParameter, TData> recorder, Mock<IParameterMapper<TParameter, TRecord, TData>> mapperMock, TRecord dataRecord)
    {
        Recorder = recorder;

        MapperMock = mapperMock;
        DataRecord = dataRecord;
    }
}
