namespace Paraminter.Mappers.MappedArgumentDataRecorderFactoryProviderCases;

using Moq;

internal static class ProviderFixtureFactory
{
    public static IProviderFixture Create()
    {
        Mock<IBoolDelegateMappedArgumentDataRecorderFactory> boolDelegateFactoryMock = new();
        Mock<IVoidDelegateMappedArgumentDataRecorderFactory> voidDelegateFactoryMock = new();

        MappedArgumentDataRecorderFactoryProvider sut = new(boolDelegateFactoryMock.Object, voidDelegateFactoryMock.Object);

        return new ProviderFixture(sut, boolDelegateFactoryMock, voidDelegateFactoryMock);
    }

    private sealed class ProviderFixture : IProviderFixture
    {
        private readonly IMappedArgumentDataRecorderFactoryProvider Sut;

        private readonly Mock<IBoolDelegateMappedArgumentDataRecorderFactory> BoolDelegateFactoryMock;
        private readonly Mock<IVoidDelegateMappedArgumentDataRecorderFactory> VoidDelegateFactoryMock;

        public ProviderFixture(IMappedArgumentDataRecorderFactoryProvider sut, Mock<IBoolDelegateMappedArgumentDataRecorderFactory> boolDelegateFactoryMock, Mock<IVoidDelegateMappedArgumentDataRecorderFactory> voidDelegateFactoryMock)
        {
            Sut = sut;

            BoolDelegateFactoryMock = boolDelegateFactoryMock;
            VoidDelegateFactoryMock = voidDelegateFactoryMock;
        }

        IMappedArgumentDataRecorderFactoryProvider IProviderFixture.Sut => Sut;

        Mock<IBoolDelegateMappedArgumentDataRecorderFactory> IProviderFixture.BoolDelegateFactoryMock => BoolDelegateFactoryMock;
        Mock<IVoidDelegateMappedArgumentDataRecorderFactory> IProviderFixture.VoidDelegateFactoryMock => VoidDelegateFactoryMock;
    }
}
