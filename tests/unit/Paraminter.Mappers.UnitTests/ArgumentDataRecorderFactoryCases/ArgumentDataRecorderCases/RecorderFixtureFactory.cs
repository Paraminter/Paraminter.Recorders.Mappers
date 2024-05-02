namespace Paraminter.Mappers.ArgumentDataRecorderFactoryCases.ArgumentDataRecorderCases;

using Moq;

internal static class RecorderFixtureFactory
{
    public static IRecorderFixture<TParameter, TRecord, TArgumentData> Create<TParameter, TRecord, TArgumentData>()
        where TRecord : class
    {
        IArgumentDataRecorderFactory factory = new ArgumentDataRecorderFactory();

        Mock<IParameterMapper<TParameter, TRecord, TArgumentData>> mapperMock = new();
        Mock<TRecord> dataRecordMock = new();

        var sut = factory.Create(mapperMock.Object, dataRecordMock.Object);

        return new RecorderFixture<TParameter, TRecord, TArgumentData>(sut, mapperMock, dataRecordMock);
    }

    private sealed class RecorderFixture<TParameter, TRecord, TArgumentData> : IRecorderFixture<TParameter, TRecord, TArgumentData>
        where TRecord : class
    {
        private readonly IArgumentDataRecorder<TParameter, TArgumentData> Sut;

        private readonly Mock<IParameterMapper<TParameter, TRecord, TArgumentData>> MapperMock;
        private readonly Mock<TRecord> DataRecordMock;

        public RecorderFixture(IArgumentDataRecorder<TParameter, TArgumentData> sut, Mock<IParameterMapper<TParameter, TRecord, TArgumentData>> mapperMock, Mock<TRecord> dataRecordMock)
        {
            Sut = sut;

            MapperMock = mapperMock;
            DataRecordMock = dataRecordMock;
        }

        IArgumentDataRecorder<TParameter, TArgumentData> IRecorderFixture<TParameter, TRecord, TArgumentData>.Sut => Sut;

        Mock<IParameterMapper<TParameter, TRecord, TArgumentData>> IRecorderFixture<TParameter, TRecord, TArgumentData>.MapperMock => MapperMock;
        Mock<TRecord> IRecorderFixture<TParameter, TRecord, TArgumentData>.DataRecordMock => DataRecordMock;
    }
}
