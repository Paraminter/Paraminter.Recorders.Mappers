namespace Attribinter.Mappers.BoolDelegateMappedArgumentRecorderFactoryCases;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        BoolDelegateMappedArgumentRecorderFactory factory = new();

        return new(factory);
    }

    public BoolDelegateMappedArgumentRecorderFactory Factory { get; }

    private FactoryContext(BoolDelegateMappedArgumentRecorderFactory factory)
    {
        Factory = factory;
    }
}
