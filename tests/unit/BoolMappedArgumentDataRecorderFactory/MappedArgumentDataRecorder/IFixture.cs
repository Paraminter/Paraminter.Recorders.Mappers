namespace Paraminter.Recorders.Mappers.MappedArgumentDataRecorder;

using Moq;

internal interface IFixture<TRecord, TArgumentData>
{
    public abstract IMappedArgumentDataRecorder<TRecord, TArgumentData> Sut { get; }

    public abstract Mock<DBoolArgumentDataRecorder<TRecord, TArgumentData>> RecorderDelegateMock { get; }
}
