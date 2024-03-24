namespace Attribinter.Mappers.ArgumentRecorderFactoryCases.T0;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        ArgumentRecorderFactory factory = new();

        return new(factory);
    }

    public IArgumentRecorderFactory Factory { get; }

    private FactoryContext(IArgumentRecorderFactory factory)
    {
        Factory = factory;
    }
}
