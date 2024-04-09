namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

using Moq;

internal interface IFactoryFixture
{
    public abstract IMappedArgumentRecorderFactory Sut { get; }

    public abstract Mock<IMappedArgumentRecorderFactoryProvider> FactoryProviderMock { get; }
}
