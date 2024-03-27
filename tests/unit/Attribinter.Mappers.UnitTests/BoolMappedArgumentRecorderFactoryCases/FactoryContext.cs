namespace Attribinter.Mappers.BoolDelegateMappedArgumentRecorderFactoryCases;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        IBoolDelegateMappedArgumentRecorderFactory factory = new BoolDelegateMappedArgumentRecorderFactory();

        return new(factory);
    }

    public IBoolDelegateMappedArgumentRecorderFactory Factory { get; }

    private FactoryContext(IBoolDelegateMappedArgumentRecorderFactory factory)
    {
        Factory = factory;
    }
}
