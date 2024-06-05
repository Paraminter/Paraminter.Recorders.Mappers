namespace Paraminter.Mappers.VoidDelegateMappedArgumentDataRecorderFactoryCases.MappedArgumentDataRecorderCases;

using Moq;

internal static class RecorderFixtureFactory
{
    public static IRecorderFixture<TRecord, TArgumentData> Create<TRecord, TArgumentData>()
    {
        IVoidDelegateMappedArgumentDataRecorderFactory factory = new VoidDelegateMappedArgumentDataRecorderFactory();

        Mock<DVoidArgumentDataRecorder<TRecord, TArgumentData>> recorderDelegateMock = new(MockBehavior.Strict);

        var sut = factory.Create(recorderDelegateMock.Object);

        return new RecorderFixture<TRecord, TArgumentData>(sut, recorderDelegateMock);
    }

    private sealed class RecorderFixture<TRecord, TArgumentData>
        : IRecorderFixture<TRecord, TArgumentData>
    {
        private readonly IMappedArgumentDataRecorder<TRecord, TArgumentData> Sut;

        private readonly Mock<DVoidArgumentDataRecorder<TRecord, TArgumentData>> RecorderDelegateMock;

        public RecorderFixture(
            IMappedArgumentDataRecorder<TRecord, TArgumentData> sut,
            Mock<DVoidArgumentDataRecorder<TRecord, TArgumentData>> recorderDelegateMock)
        {
            Sut = sut;

            RecorderDelegateMock = recorderDelegateMock;
        }

        IMappedArgumentDataRecorder<TRecord, TArgumentData> IRecorderFixture<TRecord, TArgumentData>.Sut => Sut;

        Mock<DVoidArgumentDataRecorder<TRecord, TArgumentData>> IRecorderFixture<TRecord, TArgumentData>.RecorderDelegateMock => RecorderDelegateMock;
    }
}
