namespace Paraminter.Recorders.Mappers.MappedArgumentDataRecorder;

using Moq;

internal static class FixtureFactory
{
    public static IFixture<TRecord, TArgumentData> Create<TRecord, TArgumentData>()
    {
        IBoolDelegateMappedArgumentDataRecorderFactory factory = new BoolDelegateMappedArgumentDataRecorderFactory();

        Mock<DBoolArgumentDataRecorder<TRecord, TArgumentData>> recorderDelegateMock = new() { DefaultValue = DefaultValue.Mock };

        var sut = factory.Create(recorderDelegateMock.Object);

        return new Fixture<TRecord, TArgumentData>(sut, recorderDelegateMock);
    }

    private sealed class Fixture<TRecord, TArgumentData>
        : IFixture<TRecord, TArgumentData>
    {
        private readonly IMappedArgumentDataRecorder<TRecord, TArgumentData> Sut;

        private readonly Mock<DBoolArgumentDataRecorder<TRecord, TArgumentData>> RecorderDelegateMock;

        public Fixture(
            IMappedArgumentDataRecorder<TRecord, TArgumentData> sut,
            Mock<DBoolArgumentDataRecorder<TRecord, TArgumentData>> recorderDelegateMock)
        {
            Sut = sut;

            RecorderDelegateMock = recorderDelegateMock;
        }

        IMappedArgumentDataRecorder<TRecord, TArgumentData> IFixture<TRecord, TArgumentData>.Sut => Sut;

        Mock<DBoolArgumentDataRecorder<TRecord, TArgumentData>> IFixture<TRecord, TArgumentData>.RecorderDelegateMock => RecorderDelegateMock;
    }
}
