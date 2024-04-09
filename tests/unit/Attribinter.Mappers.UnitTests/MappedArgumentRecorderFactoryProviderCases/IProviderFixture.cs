namespace Attribinter.Mappers.MappedArgumentRecorderFactoryProviderCases;

using Moq;

internal interface IProviderFixture
{
    public abstract IMappedArgumentRecorderFactoryProvider Sut { get; }

    public abstract Mock<IBoolDelegateMappedArgumentRecorderFactory> BoolDelegateFactoryMock { get; }
    public abstract Mock<IVoidDelegateMappedArgumentRecorderFactory> VoidDelegateFactoryMock { get; }
}
