namespace Paraminter.Mappers.BoolDelegateMappedArgumentDataRecorderFactoryCases.MappedArgumentDataRecorderCases;

using Moq;

internal interface IRecorderFixture<TRecord, TArgumentData>
{
    public abstract IMappedArgumentDataRecorder<TRecord, TArgumentData> Sut { get; }

    public abstract Mock<DBoolArgumentDataRecorder<TRecord, TArgumentData>> RecorderDelegateMock { get; }
}
