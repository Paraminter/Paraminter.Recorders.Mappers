namespace Paraminter.Recorders.Mappers;

using Moq;

internal interface IFixture
{
    public abstract IMappedArgumentDataRecorderFactoryProvider Sut { get; }

    public abstract Mock<IBoolDelegateMappedArgumentDataRecorderFactory> BoolDelegateMock { get; }
    public abstract Mock<IVoidDelegateMappedArgumentDataRecorderFactory> VoidDelegateMock { get; }
}
