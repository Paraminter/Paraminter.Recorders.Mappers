namespace Paraminter.Mappers.MappedArgumentDataRecorderFactoryProviderCases;

using Moq;

internal interface IProviderFixture
{
    public abstract IMappedArgumentDataRecorderFactoryProvider Sut { get; }

    public abstract Mock<IBoolDelegateMappedArgumentDataRecorderFactory> BoolDelegateFactoryMock { get; }
    public abstract Mock<IVoidDelegateMappedArgumentDataRecorderFactory> VoidDelegateFactoryMock { get; }
}
