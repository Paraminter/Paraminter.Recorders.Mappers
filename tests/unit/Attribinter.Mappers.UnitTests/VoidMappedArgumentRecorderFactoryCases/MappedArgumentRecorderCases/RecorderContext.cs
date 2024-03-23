namespace Attribinter.Mappers.VoidDelegateMappedArgumentRecorderFactoryCases.MappedArgumentRecorderCases;

using Moq;

using System;

internal sealed class RecorderContext<TRecord, TData> where TRecord : class
{
    public static RecorderContext<TRecord, TData> Create()
    {
        VoidDelegateMappedArgumentRecorderFactory factory = new();

        Mock<Action<TRecord, TData>> recorderDelegateMock = new();

        var recorder = ((IVoidDelegateMappedArgumentRecorderFactory)factory).Create(recorderDelegateMock.Object);

        return new(recorder, recorderDelegateMock);
    }

    public IMappedArgumentRecorder<TRecord, TData> Recorder { get; }

    public Mock<Action<TRecord, TData>> RecorderDelegateMock { get; }

    private RecorderContext(IMappedArgumentRecorder<TRecord, TData> recorder, Mock<Action<TRecord, TData>> recorderDelegateMock)
    {
        Recorder = recorder;

        RecorderDelegateMock = recorderDelegateMock;
    }
}
