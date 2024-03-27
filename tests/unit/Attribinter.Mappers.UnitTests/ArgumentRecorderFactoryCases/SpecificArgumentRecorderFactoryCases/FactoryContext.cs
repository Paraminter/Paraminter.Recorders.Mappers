namespace Attribinter.Mappers.ArgumentRecorderFactoryCases.SpecificArgumentRecorderFactoryCases;

using Attribinter.Mappers;

using Moq;

internal sealed class FactoryContext<TParameter, TRecord, TData>
{
    public static FactoryContext<TParameter, TRecord, TData> Create()
    {
        IArgumentRecorderFactory factory = new ArgumentRecorderFactory();

        var mapper = Mock.Of<IParameterMapper<TParameter, TRecord, TData>>();

        var specificFactory = factory.WithMapper(mapper);

        return new(specificFactory, mapper);
    }

    public IArgumentRecorderFactory<TParameter, TRecord, TData> Factory { get; }

    public IParameterMapper<TParameter, TRecord, TData> Mapper { get; }

    private FactoryContext(IArgumentRecorderFactory<TParameter, TRecord, TData> factory, IParameterMapper<TParameter, TRecord, TData> mapper)
    {
        Factory = factory;

        Mapper = mapper;
    }
}
