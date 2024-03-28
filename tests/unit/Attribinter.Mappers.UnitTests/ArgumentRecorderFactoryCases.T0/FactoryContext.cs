namespace Attribinter.Mappers.ArgumentRecorderFactoryCases.T0;

using Attribinter.Mappers;

internal sealed class FactoryContext<TParameter, TRecord, TData>
{
    public static FactoryContext<TParameter, TRecord, TData> Create()
    {
        IArgumentRecorderFactory factory = new ArgumentRecorderFactory();

        return new(factory);
    }

    public IArgumentRecorderFactory Factory { get; }

    private FactoryContext(IArgumentRecorderFactory factory)
    {
        Factory = factory;
    }
}
