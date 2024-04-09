namespace Attribinter.Mappers.ArgumentRecorderFactoryCases;

using Moq;

internal interface IFactoryFixture<TParameter, TRecord, TData>
{
    public abstract IArgumentRecorderFactory<TParameter, TRecord, TData> Sut { get; }

    public abstract Mock<IParameterMapper<TParameter, TRecord, TData>> MapperMock { get; }
}
