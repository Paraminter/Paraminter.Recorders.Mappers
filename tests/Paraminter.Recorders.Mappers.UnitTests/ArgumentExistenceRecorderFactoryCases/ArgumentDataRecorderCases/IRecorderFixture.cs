namespace Paraminter.Mappers.ArgumentExistenceRecorderFactoryCases.ArgumentExistenceRecorderCases;

using Moq;

internal interface IRecorderFixture<TParameter, TRecord>
    where TRecord : class
{
    public abstract IArgumentExistenceRecorder<TParameter> Sut { get; }

    public abstract Mock<IArgumentExistenceRecorderMapper<TParameter, TRecord>> MapperMock { get; }
    public abstract Mock<TRecord> DataRecordMock { get; }
}
