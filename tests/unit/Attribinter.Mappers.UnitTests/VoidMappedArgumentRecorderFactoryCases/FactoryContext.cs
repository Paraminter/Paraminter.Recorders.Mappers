namespace Attribinter.Mappers.VoidDelegateMappedArgumentRecorderFactoryCases;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        IVoidDelegateMappedArgumentRecorderFactory factory = new VoidDelegateMappedArgumentRecorderFactory();

        return new(factory);
    }

    public IVoidDelegateMappedArgumentRecorderFactory Factory { get; }

    private FactoryContext(IVoidDelegateMappedArgumentRecorderFactory factory)
    {
        Factory = factory;
    }
}
