namespace Paraminter.Recorders.Mappers.ArgumentDataRecorder;

using Moq;

internal interface IFixture<TParameter, TRecord, TArgumentData>
    where TRecord : class
{
    public abstract IArgumentDataRecorder<TParameter, TArgumentData> Sut { get; }

    public abstract Mock<IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData>> MapperMock { get; }
    public abstract Mock<TRecord> DataRecordMock { get; }
}
