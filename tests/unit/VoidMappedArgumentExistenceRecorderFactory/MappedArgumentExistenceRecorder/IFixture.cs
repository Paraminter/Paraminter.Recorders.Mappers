namespace Paraminter.Recorders.Mappers.MappedArgumentExistenceRecorder;

using Moq;

internal interface IFixture<TRecord>
{
    public abstract IMappedArgumentExistenceRecorder<TRecord> Sut { get; }

    public abstract Mock<DVoidArgumentExistenceRecorder<TRecord>> RecorderDelegateMock { get; }
}
