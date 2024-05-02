namespace Paraminter.Mappers.VoidDelegateMappedArgumentDataRecorderFactoryCases.MappedArgumentDataRecorderCases;

using Moq;

internal interface IRecorderFixture<TRecord, TArgumentData>
{
    public abstract IMappedArgumentDataRecorder<TRecord, TArgumentData> Sut { get; }

    public abstract Mock<DVoidArgumentDataRecorder<TRecord, TArgumentData>> RecorderDelegateMock { get; }
}
