namespace Paraminter.Mappers.BoolDelegateMappedArgumentExistenceRecorderFactoryCases.MappedArgumentExistenceRecorderCases;

using Moq;

internal interface IRecorderFixture<TRecord>
{
    public abstract IMappedArgumentExistenceRecorder<TRecord> Sut { get; }

    public abstract Mock<DBoolArgumentExistenceRecorder<TRecord>> RecorderDelegateMock { get; }
}
