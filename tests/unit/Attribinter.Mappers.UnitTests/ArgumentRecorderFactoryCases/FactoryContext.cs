namespace Attribinter.Mappers.ArgumentRecorderFactoryCases;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        ArgumentRecorderFactory factory = new();

        return new(factory);
    }

    public ArgumentRecorderFactory Factory { get; }

    private FactoryContext(ArgumentRecorderFactory factory)
    {
        Factory = factory;
    }
}
