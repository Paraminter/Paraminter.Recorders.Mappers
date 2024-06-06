namespace Paraminter.Recorders.Mappers.MappedArgumentExistenceRecorderFactoryProviderCases;

using Moq;

internal interface IProviderFixture
{
    public abstract IMappedArgumentExistenceRecorderFactoryProvider Sut { get; }

    public abstract Mock<IBoolDelegateMappedArgumentExistenceRecorderFactory> BoolDelegateMock { get; }
    public abstract Mock<IVoidDelegateMappedArgumentExistenceRecorderFactory> VoidDelegateMock { get; }
}
