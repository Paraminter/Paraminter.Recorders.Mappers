namespace Paraminter.Recorders.Mappers;

using Moq;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        Mock<IBoolDelegateMappedArgumentDataRecorderFactory> boolDelegateMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IVoidDelegateMappedArgumentDataRecorderFactory> voidDelegateMock = new() { DefaultValue = DefaultValue.Mock };

        MappedArgumentDataRecorderFactoryProvider sut = new(boolDelegateMock.Object, voidDelegateMock.Object);

        return new Fixture(sut, boolDelegateMock, voidDelegateMock);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly IMappedArgumentDataRecorderFactoryProvider Sut;

        private readonly Mock<IBoolDelegateMappedArgumentDataRecorderFactory> BoolDelegateMock;
        private readonly Mock<IVoidDelegateMappedArgumentDataRecorderFactory> VoidDelegateMock;

        public Fixture(
            IMappedArgumentDataRecorderFactoryProvider sut,
            Mock<IBoolDelegateMappedArgumentDataRecorderFactory> boolDelegateMock,
            Mock<IVoidDelegateMappedArgumentDataRecorderFactory> voidDelegateMock)
        {
            Sut = sut;

            BoolDelegateMock = boolDelegateMock;
            VoidDelegateMock = voidDelegateMock;
        }

        IMappedArgumentDataRecorderFactoryProvider IFixture.Sut => Sut;

        Mock<IBoolDelegateMappedArgumentDataRecorderFactory> IFixture.BoolDelegateMock => BoolDelegateMock;
        Mock<IVoidDelegateMappedArgumentDataRecorderFactory> IFixture.VoidDelegateMock => VoidDelegateMock;
    }
}
