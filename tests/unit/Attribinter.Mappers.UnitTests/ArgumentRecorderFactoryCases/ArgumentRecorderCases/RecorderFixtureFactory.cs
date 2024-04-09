namespace Attribinter.Mappers.ArgumentRecorderFactoryCases.ArgumentRecorderCases;

using Moq;

internal static class RecorderFixtureFactory
{
    public static IRecorderFixture<TParameter, TRecord, TData> Create<TParameter, TRecord, TData>() where TRecord : class
    {
        Mock<IParameterMapper<TParameter, TRecord, TData>> mapperMock = new();

        IArgumentRecorderFactory<TParameter, TRecord, TData> factory = new ArgumentRecorderFactory<TParameter, TRecord, TData>(mapperMock.Object);

        var dataRecord = Mock.Of<TRecord>();

        var sut = factory.Create(dataRecord);

        return new RecorderFixture<TParameter, TRecord, TData>(sut, mapperMock, dataRecord);
    }

    private sealed class RecorderFixture<TParameter, TRecord, TData> : IRecorderFixture<TParameter, TRecord, TData> where TRecord : class
    {
        private readonly IArgumentRecorder<TParameter, TData> Sut;

        private readonly Mock<IParameterMapper<TParameter, TRecord, TData>> MapperMock;
        private readonly TRecord DataRecord;

        public RecorderFixture(IArgumentRecorder<TParameter, TData> sut, Mock<IParameterMapper<TParameter, TRecord, TData>> mapperMock, TRecord dataRecord)
        {
            Sut = sut;

            MapperMock = mapperMock;
            DataRecord = dataRecord;
        }

        IArgumentRecorder<TParameter, TData> IRecorderFixture<TParameter, TRecord, TData>.Sut => Sut;

        Mock<IParameterMapper<TParameter, TRecord, TData>> IRecorderFixture<TParameter, TRecord, TData>.MapperMock => MapperMock;
        TRecord IRecorderFixture<TParameter, TRecord, TData>.DataRecord => DataRecord;
    }
}
