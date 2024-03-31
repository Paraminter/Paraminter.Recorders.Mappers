namespace Attribinter.Mappers.MappedArgumentRecorderFactoryProviderCases;

using Moq;

internal sealed class ProviderContext
{
    public static ProviderContext Create()
    {
        var boolDelegateFactory = Mock.Of<IBoolDelegateMappedArgumentRecorderFactory>();
        var voidDelegateFactory = Mock.Of<IVoidDelegateMappedArgumentRecorderFactory>();

        IMappedArgumentRecorderFactoryProvider provider = new MappedArgumentRecorderFactoryProvider(boolDelegateFactory, voidDelegateFactory);

        return new(provider, boolDelegateFactory, voidDelegateFactory);
    }

    public IMappedArgumentRecorderFactoryProvider Provider { get; }

    public IBoolDelegateMappedArgumentRecorderFactory BoolDelegateFactory { get; }
    public IVoidDelegateMappedArgumentRecorderFactory VoidDelegateFactory { get; }

    private ProviderContext(IMappedArgumentRecorderFactoryProvider provider, IBoolDelegateMappedArgumentRecorderFactory boolDelegateFactory, IVoidDelegateMappedArgumentRecorderFactory voidDelegateFactory)
    {
        Provider = provider;

        BoolDelegateFactory = boolDelegateFactory;
        VoidDelegateFactory = voidDelegateFactory;
    }
}
