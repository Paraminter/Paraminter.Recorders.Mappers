namespace Attribinter.Mappers.VoidDelegateMappedArgumentRecorderFactoryCases.MappedArgumentRecorderCases;

using Moq;

internal interface IRecorderFixture<TRecord, TData>
{
    public abstract IMappedArgumentRecorder<TRecord, TData> Sut { get; }

    public abstract Mock<DCertainArgumentRecorder<TRecord, TData>> RecorderDelegateMock { get; }
}
