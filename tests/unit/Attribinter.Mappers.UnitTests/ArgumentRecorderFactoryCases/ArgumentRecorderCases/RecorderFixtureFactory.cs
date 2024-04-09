namespace Attribinter.Mappers.ArgumentRecorderFactoryCases.ArgumentRecorderCases;

using Moq;

internal static class RecorderFixtureFactory
{
    public static IRecorderFixture<TParameter, TRecord, TData> Create<TParameter, TRecord, TData>() where TRecord : class
    {
        IArgumentRecorderFactory factory = new ArgumentRecorderFactory();

        Mock<IParameterMapper<TParameter, TRecord, TData>> mapperMock = new();
        Mock<TRecord> dataRecordMock = new();

        var sut = factory.Create(mapperMock.Object, dataRecordMock.Object);

        return new RecorderFixture<TParameter, TRecord, TData>(sut, mapperMock, dataRecordMock);
    }

    private sealed class RecorderFixture<TParameter, TRecord, TData> : IRecorderFixture<TParameter, TRecord, TData> where TRecord : class
    {
        private readonly IArgumentRecorder<TParameter, TData> Sut;

        private readonly Mock<IParameterMapper<TParameter, TRecord, TData>> MapperMock;
        private readonly Mock<TRecord> DataRecordMock;

        public RecorderFixture(IArgumentRecorder<TParameter, TData> sut, Mock<IParameterMapper<TParameter, TRecord, TData>> mapperMock, Mock<TRecord> dataRecordMock)
        {
            Sut = sut;

            MapperMock = mapperMock;
            DataRecordMock = dataRecordMock;
        }

        IArgumentRecorder<TParameter, TData> IRecorderFixture<TParameter, TRecord, TData>.Sut => Sut;

        Mock<IParameterMapper<TParameter, TRecord, TData>> IRecorderFixture<TParameter, TRecord, TData>.MapperMock => MapperMock;
        Mock<TRecord> IRecorderFixture<TParameter, TRecord, TData>.DataRecordMock => DataRecordMock;
    }
}
