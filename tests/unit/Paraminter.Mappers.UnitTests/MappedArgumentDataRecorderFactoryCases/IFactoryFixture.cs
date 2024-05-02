namespace Paraminter.Mappers.MappedArgumentDataRecorderFactoryCases;

using Moq;

internal interface IFactoryFixture
{
    public abstract IMappedArgumentDataRecorderFactory Sut { get; }

    public abstract Mock<IMappedArgumentDataRecorderFactoryProvider> FactoryProviderMock { get; }
}
