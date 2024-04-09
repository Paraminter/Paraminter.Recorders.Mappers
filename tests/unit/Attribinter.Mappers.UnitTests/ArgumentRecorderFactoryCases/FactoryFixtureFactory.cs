namespace Attribinter.Mappers.ArgumentRecorderFactoryCases;

using Moq;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture<TParameter, TRecord, TData> Create<TParameter, TRecord, TData>()
    {
        Mock<IParameterMapper<TParameter, TRecord, TData>> mapperMock = new();

        ArgumentRecorderFactory<TParameter, TRecord, TData> sut = new(mapperMock.Object);

        return new FactoryFixture<TParameter, TRecord, TData>(sut, mapperMock);
    }

    private sealed class FactoryFixture<TParameter, TRecord, TData> : IFactoryFixture<TParameter, TRecord, TData>
    {
        private readonly IArgumentRecorderFactory<TParameter, TRecord, TData> Sut;

        private readonly Mock<IParameterMapper<TParameter, TRecord, TData>> MapperMock;

        public FactoryFixture(IArgumentRecorderFactory<TParameter, TRecord, TData> sut, Mock<IParameterMapper<TParameter, TRecord, TData>> mapperMock)
        {
            Sut = sut;

            MapperMock = mapperMock;
        }

        IArgumentRecorderFactory<TParameter, TRecord, TData> IFactoryFixture<TParameter, TRecord, TData>.Sut => Sut;

        Mock<IParameterMapper<TParameter, TRecord, TData>> IFactoryFixture<TParameter, TRecord, TData>.MapperMock => MapperMock;
    }
}
