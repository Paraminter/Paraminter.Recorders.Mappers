namespace Paraminter.Mappers.MappedArgumentDataRecorderFactoryCases;

using Moq;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        Mock<IMappedArgumentDataRecorderFactoryProvider> factoryProviderMock = new() { DefaultValue = DefaultValue.Mock };

        MappedArgumentDataRecorderFactory sut = new(factoryProviderMock.Object);

        return new FactoryFixture(sut, factoryProviderMock);
    }

    private sealed class FactoryFixture : IFactoryFixture
    {
        private readonly IMappedArgumentDataRecorderFactory Sut;

        private readonly Mock<IMappedArgumentDataRecorderFactoryProvider> FactoryMockProviderMock;

        public FactoryFixture(IMappedArgumentDataRecorderFactory sut, Mock<IMappedArgumentDataRecorderFactoryProvider> factoryMockProviderMock)
        {
            Sut = sut;

            FactoryMockProviderMock = factoryMockProviderMock;
        }

        IMappedArgumentDataRecorderFactory IFactoryFixture.Sut => Sut;

        Mock<IMappedArgumentDataRecorderFactoryProvider> IFactoryFixture.FactoryProviderMock => FactoryMockProviderMock;
    }
}
