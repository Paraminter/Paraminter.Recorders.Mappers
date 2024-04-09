namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

using Moq;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        Mock<IMappedArgumentRecorderFactoryProvider> factoryProviderMock = new() { DefaultValue = DefaultValue.Mock };

        MappedArgumentRecorderFactory factory = new(factoryProviderMock.Object);

        return new FactoryFixture(factory, factoryProviderMock);
    }

    private sealed class FactoryFixture : IFactoryFixture
    {
        private readonly IMappedArgumentRecorderFactory Sut;

        private readonly Mock<IMappedArgumentRecorderFactoryProvider> FactoryMockProviderMock;

        public FactoryFixture(IMappedArgumentRecorderFactory sut, Mock<IMappedArgumentRecorderFactoryProvider> factoryMockProviderMock)
        {
            Sut = sut;

            FactoryMockProviderMock = factoryMockProviderMock;
        }

        IMappedArgumentRecorderFactory IFactoryFixture.Sut => Sut;

        Mock<IMappedArgumentRecorderFactoryProvider> IFactoryFixture.FactoryProviderMock => FactoryMockProviderMock;
    }
}
