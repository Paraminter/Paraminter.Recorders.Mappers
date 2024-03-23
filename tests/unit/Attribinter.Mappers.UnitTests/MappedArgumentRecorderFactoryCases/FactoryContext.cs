namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        MappedArgumentRecorderFactory factory = new();

        return new(factory);
    }

    public MappedArgumentRecorderFactory Factory { get; }

    private FactoryContext(MappedArgumentRecorderFactory factory)
    {
        Factory = factory;
    }
}
