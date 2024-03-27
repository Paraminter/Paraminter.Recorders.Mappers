namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

using Moq;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        var boolDelegateFactory = Mock.Of<IBoolDelegateMappedArgumentRecorderFactory>();
        var voidDelegateFactory = Mock.Of<IVoidDelegateMappedArgumentRecorderFactory>();

        IMappedArgumentRecorderFactory factory = new MappedArgumentRecorderFactory(boolDelegateFactory, voidDelegateFactory);

        return new(factory, boolDelegateFactory, voidDelegateFactory);
    }

    public IMappedArgumentRecorderFactory Factory { get; }

    public IBoolDelegateMappedArgumentRecorderFactory BoolDelegateFactory { get; }
    public IVoidDelegateMappedArgumentRecorderFactory VoidDelegateFactory { get; }

    private FactoryContext(IMappedArgumentRecorderFactory factory, IBoolDelegateMappedArgumentRecorderFactory boolDelegateFactory, IVoidDelegateMappedArgumentRecorderFactory voidDelegateFactory)
    {
        Factory = factory;

        BoolDelegateFactory = boolDelegateFactory;
        VoidDelegateFactory = voidDelegateFactory;
    }
}
