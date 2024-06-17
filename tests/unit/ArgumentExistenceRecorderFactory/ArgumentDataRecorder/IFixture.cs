namespace Paraminter.Recorders.Mappers.ArgumentExistenceRecorder;

using Moq;

internal interface IFixture<TParameter, TRecord>
    where TRecord : class
{
    public abstract IArgumentExistenceRecorder<TParameter> Sut { get; }

    public abstract Mock<IArgumentExistenceRecorderMapper<TParameter, TRecord>> MapperMock { get; }
    public abstract Mock<TRecord> DataRecordMock { get; }
}
