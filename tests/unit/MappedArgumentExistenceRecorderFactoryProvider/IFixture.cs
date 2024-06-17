namespace Paraminter.Recorders.Mappers;

using Moq;

internal interface IFixture
{
    public abstract IMappedArgumentExistenceRecorderFactoryProvider Sut { get; }

    public abstract Mock<IBoolDelegateMappedArgumentExistenceRecorderFactory> BoolDelegateMock { get; }
    public abstract Mock<IVoidDelegateMappedArgumentExistenceRecorderFactory> VoidDelegateMock { get; }
}
