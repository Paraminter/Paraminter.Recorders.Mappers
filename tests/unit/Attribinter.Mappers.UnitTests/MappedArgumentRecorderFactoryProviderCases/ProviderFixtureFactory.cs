namespace Attribinter.Mappers.MappedArgumentRecorderFactoryProviderCases;

using Moq;

internal static class ProviderFixtureFactory
{
    public static IProviderFixture Create()
    {
        Mock<IBoolDelegateMappedArgumentRecorderFactory> boolDelegateFactoryMock = new();
        Mock<IVoidDelegateMappedArgumentRecorderFactory> voidDelegateFactoryMock = new();

        MappedArgumentRecorderFactoryProvider sut = new(boolDelegateFactoryMock.Object, voidDelegateFactoryMock.Object);

        return new ProviderFixture(sut, boolDelegateFactoryMock, voidDelegateFactoryMock);
    }

    private sealed class ProviderFixture : IProviderFixture
    {
        private readonly IMappedArgumentRecorderFactoryProvider Sut;

        private readonly Mock<IBoolDelegateMappedArgumentRecorderFactory> BoolDelegateFactoryMock;
        private readonly Mock<IVoidDelegateMappedArgumentRecorderFactory> VoidDelegateFactoryMock;

        public ProviderFixture(IMappedArgumentRecorderFactoryProvider sut, Mock<IBoolDelegateMappedArgumentRecorderFactory> boolDelegateFactoryMock, Mock<IVoidDelegateMappedArgumentRecorderFactory> voidDelegateFactoryMock)
        {
            Sut = sut;

            BoolDelegateFactoryMock = boolDelegateFactoryMock;
            VoidDelegateFactoryMock = voidDelegateFactoryMock;
        }

        IMappedArgumentRecorderFactoryProvider IProviderFixture.Sut => Sut;

        Mock<IBoolDelegateMappedArgumentRecorderFactory> IProviderFixture.BoolDelegateFactoryMock => BoolDelegateFactoryMock;
        Mock<IVoidDelegateMappedArgumentRecorderFactory> IProviderFixture.VoidDelegateFactoryMock => VoidDelegateFactoryMock;
    }
}
