namespace Paraminter.Mappers.MappedArgumentDataRecorderFactoryProviderCases;

using Moq;

internal static class ProviderFixtureFactory
{
    public static IProviderFixture Create()
    {
        Mock<IBoolDelegateMappedArgumentDataRecorderFactory> boolDelegateMock = new();
        Mock<IVoidDelegateMappedArgumentDataRecorderFactory> voidDelegateMock = new();

        MappedArgumentDataRecorderFactoryProvider sut = new(boolDelegateMock.Object, voidDelegateMock.Object);

        return new ProviderFixture(sut, boolDelegateMock, voidDelegateMock);
    }

    private sealed class ProviderFixture : IProviderFixture
    {
        private readonly IMappedArgumentDataRecorderFactoryProvider Sut;

        private readonly Mock<IBoolDelegateMappedArgumentDataRecorderFactory> BoolDelegateMock;
        private readonly Mock<IVoidDelegateMappedArgumentDataRecorderFactory> VoidDelegateMock;

        public ProviderFixture(IMappedArgumentDataRecorderFactoryProvider sut, Mock<IBoolDelegateMappedArgumentDataRecorderFactory> boolDelegateMock, Mock<IVoidDelegateMappedArgumentDataRecorderFactory> voidDelegateMock)
        {
            Sut = sut;

            BoolDelegateMock = boolDelegateMock;
            VoidDelegateMock = voidDelegateMock;
        }

        IMappedArgumentDataRecorderFactoryProvider IProviderFixture.Sut => Sut;

        Mock<IBoolDelegateMappedArgumentDataRecorderFactory> IProviderFixture.BoolDelegateMock => BoolDelegateMock;
        Mock<IVoidDelegateMappedArgumentDataRecorderFactory> IProviderFixture.VoidDelegateMock => VoidDelegateMock;
    }
}
