namespace Attribinter.Mappers.BoolDelegateMappedArgumentRecorderFactoryCases.MappedArgumentRecorderCases;

using Moq;

using System;

internal interface IRecorderFixture<TRecord, TData>
{
    public abstract IMappedArgumentRecorder<TRecord, TData> Sut { get; }

    public abstract Mock<DAttemptingArgumentRecorder<TRecord, TData>> RecorderDelegateMock { get; }
}
