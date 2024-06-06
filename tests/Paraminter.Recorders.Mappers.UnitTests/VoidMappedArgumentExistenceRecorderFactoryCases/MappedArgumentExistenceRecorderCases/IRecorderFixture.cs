namespace Paraminter.Mappers.VoidDelegateMappedArgumentExistenceRecorderFactoryCases.MappedArgumentExistenceRecorderCases;

using Moq;

internal interface IRecorderFixture<TRecord>
{
    public abstract IMappedArgumentExistenceRecorder<TRecord> Sut { get; }

    public abstract Mock<DVoidArgumentExistenceRecorder<TRecord>> RecorderDelegateMock { get; }
}
