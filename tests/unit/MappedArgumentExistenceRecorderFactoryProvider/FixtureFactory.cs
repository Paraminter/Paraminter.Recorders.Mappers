namespace Paraminter.Recorders.Mappers;

using Moq;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        Mock<IBoolDelegateMappedArgumentExistenceRecorderFactory> boolDelegateMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IVoidDelegateMappedArgumentExistenceRecorderFactory> voidDelegateMock = new() { DefaultValue = DefaultValue.Mock };

        MappedArgumentExistenceRecorderFactoryProvider sut = new(boolDelegateMock.Object, voidDelegateMock.Object);

        return new Fixture(sut, boolDelegateMock, voidDelegateMock);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly IMappedArgumentExistenceRecorderFactoryProvider Sut;

        private readonly Mock<IBoolDelegateMappedArgumentExistenceRecorderFactory> BoolDelegateMock;
        private readonly Mock<IVoidDelegateMappedArgumentExistenceRecorderFactory> VoidDelegateMock;

        public Fixture(
            IMappedArgumentExistenceRecorderFactoryProvider sut,
            Mock<IBoolDelegateMappedArgumentExistenceRecorderFactory> boolDelegateMock,
            Mock<IVoidDelegateMappedArgumentExistenceRecorderFactory> voidDelegateMock)
        {
            Sut = sut;

            BoolDelegateMock = boolDelegateMock;
            VoidDelegateMock = voidDelegateMock;
        }

        IMappedArgumentExistenceRecorderFactoryProvider IFixture.Sut => Sut;

        Mock<IBoolDelegateMappedArgumentExistenceRecorderFactory> IFixture.BoolDelegateMock => BoolDelegateMock;
        Mock<IVoidDelegateMappedArgumentExistenceRecorderFactory> IFixture.VoidDelegateMock => VoidDelegateMock;
    }
}
