namespace Attribinter.Mappers.ArgumentRecorderFactoryCases;

using Moq;

internal sealed class FactoryContext<TParameter, TRecord, TData>
{
    public static FactoryContext<TParameter, TRecord, TData> Create()
    {
        var mapper = Mock.Of<IParameterMapper<TParameter, TRecord, TData>>();

        ArgumentRecorderFactory<TParameter, TRecord, TData> factory = new(mapper);

        return new(factory, mapper);
    }

    public IArgumentRecorderFactory<TParameter, TRecord, TData> Factory { get; }

    public IParameterMapper<TParameter, TRecord, TData> Mapper { get; }

    private FactoryContext(IArgumentRecorderFactory<TParameter, TRecord, TData> factory, IParameterMapper<TParameter, TRecord, TData> mapper)
    {
        Factory = factory;

        Mapper = mapper;
    }
}
