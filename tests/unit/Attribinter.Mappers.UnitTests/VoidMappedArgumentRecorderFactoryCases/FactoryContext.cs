namespace Attribinter.Mappers.VoidDelegateMappedArgumentRecorderFactoryCases;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        VoidDelegateMappedArgumentRecorderFactory factory = new();

        return new(factory);
    }

    public VoidDelegateMappedArgumentRecorderFactory Factory { get; }

    private FactoryContext(VoidDelegateMappedArgumentRecorderFactory factory)
    {
        Factory = factory;
    }
}
