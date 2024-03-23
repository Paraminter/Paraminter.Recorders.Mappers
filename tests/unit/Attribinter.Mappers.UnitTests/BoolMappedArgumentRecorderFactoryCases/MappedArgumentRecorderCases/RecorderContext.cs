namespace Attribinter.Mappers.BoolDelegateMappedArgumentRecorderFactoryCases.MappedArgumentRecorderCases;

using Moq;

using System;

internal sealed class RecorderContext<TRecord, TData> where TRecord : class
{
    public static RecorderContext<TRecord, TData> Create()
    {
        BoolDelegateMappedArgumentRecorderFactory factory = new();

        Mock<Func<TRecord, TData, bool>> recorderDelegateMock = new();

        var recorder = ((IBoolDelegateMappedArgumentRecorderFactory)factory).Create(recorderDelegateMock.Object);

        return new(recorder, recorderDelegateMock);
    }

    public IMappedArgumentRecorder<TRecord, TData> Recorder { get; }

    public Mock<Func<TRecord, TData, bool>> RecorderDelegateMock { get; }

    private RecorderContext(IMappedArgumentRecorder<TRecord, TData> recorder, Mock<Func<TRecord, TData, bool>> recorderDelegateMock)
    {
        Recorder = recorder;

        RecorderDelegateMock = recorderDelegateMock;
    }
}
