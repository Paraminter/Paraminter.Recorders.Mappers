namespace Paraminter.Recorders.Mappers.ArgumentDataRecorder;

using Moq;

internal static class FixtureFactory
{
    public static IFixture<TParameter, TRecord, TArgumentData> Create<TParameter, TRecord, TArgumentData>()
        where TRecord : class
    {
        IArgumentDataRecorderFactory factory = new ArgumentDataRecorderFactory();

        Mock<IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData>> mapperMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<TRecord> dataRecordMock = new() { DefaultValue = DefaultValue.Mock };

        var sut = factory.Create(mapperMock.Object, dataRecordMock.Object);

        return new Fixture<TParameter, TRecord, TArgumentData>(sut, mapperMock, dataRecordMock);
    }

    private sealed class Fixture<TParameter, TRecord, TArgumentData>
        : IFixture<TParameter, TRecord, TArgumentData>
        where TRecord : class
    {
        private readonly IArgumentDataRecorder<TParameter, TArgumentData> Sut;

        private readonly Mock<IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData>> MapperMock;
        private readonly Mock<TRecord> DataRecordMock;

        public Fixture(
            IArgumentDataRecorder<TParameter, TArgumentData> sut,
            Mock<IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData>> mapperMock,
            Mock<TRecord> dataRecordMock)
        {
            Sut = sut;

            MapperMock = mapperMock;
            DataRecordMock = dataRecordMock;
        }

        IArgumentDataRecorder<TParameter, TArgumentData> IFixture<TParameter, TRecord, TArgumentData>.Sut => Sut;

        Mock<IArgumentDataRecorderMapper<TParameter, TRecord, TArgumentData>> IFixture<TParameter, TRecord, TArgumentData>.MapperMock => MapperMock;
        Mock<TRecord> IFixture<TParameter, TRecord, TArgumentData>.DataRecordMock => DataRecordMock;
    }
}
