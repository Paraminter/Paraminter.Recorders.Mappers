namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

using Moq;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        Mock<IMappedArgumentRecorderFactoryProvider> factoryProviderMock = new() { DefaultValue = DefaultValue.Mock };

        IMappedArgumentRecorderFactory factory = new MappedArgumentRecorderFactory(factoryProviderMock.Object);

        return new(factory, factoryProviderMock);
    }

    public IMappedArgumentRecorderFactory Factory { get; }

    public Mock<IMappedArgumentRecorderFactoryProvider> FactoryProviderMock { get; }

    private FactoryContext(IMappedArgumentRecorderFactory factory, Mock<IMappedArgumentRecorderFactoryProvider> factoryProviderMock)
    {
        Factory = factory;

        FactoryProviderMock = factoryProviderMock;
    }
}
