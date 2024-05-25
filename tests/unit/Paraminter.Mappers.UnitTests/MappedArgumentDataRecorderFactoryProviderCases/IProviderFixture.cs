namespace Paraminter.Mappers.MappedArgumentDataRecorderFactoryProviderCases;

using Moq;

internal interface IProviderFixture
{
    public abstract IMappedArgumentDataRecorderFactoryProvider Sut { get; }

    public abstract Mock<IBoolDelegateMappedArgumentDataRecorderFactory> BoolDelegateMock { get; }
    public abstract Mock<IVoidDelegateMappedArgumentDataRecorderFactory> VoidDelegateMock { get; }
}
