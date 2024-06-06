namespace Paraminter.Recorders.Mappers.BoolDelegateMappedArgumentDataRecorderFactoryCases.MappedArgumentDataRecorderCases;

using Moq;

internal static class RecorderFixtureFactory
{
    public static IRecorderFixture<TRecord, TArgumentData> Create<TRecord, TArgumentData>()
    {
        IBoolDelegateMappedArgumentDataRecorderFactory factory = new BoolDelegateMappedArgumentDataRecorderFactory();

        Mock<DBoolArgumentDataRecorder<TRecord, TArgumentData>> recorderDelegateMock = new() { DefaultValue = DefaultValue.Mock };

        var sut = factory.Create(recorderDelegateMock.Object);

        return new RecorderFixture<TRecord, TArgumentData>(sut, recorderDelegateMock);
    }

    private sealed class RecorderFixture<TRecord, TArgumentData>
        : IRecorderFixture<TRecord, TArgumentData>
    {
        private readonly IMappedArgumentDataRecorder<TRecord, TArgumentData> Sut;

        private readonly Mock<DBoolArgumentDataRecorder<TRecord, TArgumentData>> RecorderDelegateMock;

        public RecorderFixture(
            IMappedArgumentDataRecorder<TRecord, TArgumentData> sut,
            Mock<DBoolArgumentDataRecorder<TRecord, TArgumentData>> recorderDelegateMock)
        {
            Sut = sut;

            RecorderDelegateMock = recorderDelegateMock;
        }

        IMappedArgumentDataRecorder<TRecord, TArgumentData> IRecorderFixture<TRecord, TArgumentData>.Sut => Sut;

        Mock<DBoolArgumentDataRecorder<TRecord, TArgumentData>> IRecorderFixture<TRecord, TArgumentData>.RecorderDelegateMock => RecorderDelegateMock;
    }
}
