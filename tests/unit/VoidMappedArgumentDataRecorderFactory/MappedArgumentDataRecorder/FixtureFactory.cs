namespace Paraminter.Recorders.Mappers.MappedArgumentDataRecorder;

using Moq;

internal static class FixtureFactory
{
    public static IFixture<TRecord, TArgumentData> Create<TRecord, TArgumentData>()
    {
        IVoidDelegateMappedArgumentDataRecorderFactory factory = new VoidDelegateMappedArgumentDataRecorderFactory();

        Mock<DVoidArgumentDataRecorder<TRecord, TArgumentData>> recorderDelegateMock = new() { DefaultValue = DefaultValue.Mock };

        var sut = factory.Create(recorderDelegateMock.Object);

        return new Fixture<TRecord, TArgumentData>(sut, recorderDelegateMock);
    }

    private sealed class Fixture<TRecord, TArgumentData>
        : IFixture<TRecord, TArgumentData>
    {
        private readonly IMappedArgumentDataRecorder<TRecord, TArgumentData> Sut;

        private readonly Mock<DVoidArgumentDataRecorder<TRecord, TArgumentData>> RecorderDelegateMock;

        public Fixture(
            IMappedArgumentDataRecorder<TRecord, TArgumentData> sut,
            Mock<DVoidArgumentDataRecorder<TRecord, TArgumentData>> recorderDelegateMock)
        {
            Sut = sut;

            RecorderDelegateMock = recorderDelegateMock;
        }

        IMappedArgumentDataRecorder<TRecord, TArgumentData> IFixture<TRecord, TArgumentData>.Sut => Sut;

        Mock<DVoidArgumentDataRecorder<TRecord, TArgumentData>> IFixture<TRecord, TArgumentData>.RecorderDelegateMock => RecorderDelegateMock;
    }
}
