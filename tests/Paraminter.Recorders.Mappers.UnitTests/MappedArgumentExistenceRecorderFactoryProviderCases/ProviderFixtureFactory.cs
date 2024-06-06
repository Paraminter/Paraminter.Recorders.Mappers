namespace Paraminter.Recorders.Mappers.MappedArgumentExistenceRecorderFactoryProviderCases;

using Moq;

internal static class ProviderFixtureFactory
{
    public static IProviderFixture Create()
    {
        Mock<IBoolDelegateMappedArgumentExistenceRecorderFactory> boolDelegateMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IVoidDelegateMappedArgumentExistenceRecorderFactory> voidDelegateMock = new() { DefaultValue = DefaultValue.Mock };

        MappedArgumentExistenceRecorderFactoryProvider sut = new(boolDelegateMock.Object, voidDelegateMock.Object);

        return new ProviderFixture(sut, boolDelegateMock, voidDelegateMock);
    }

    private sealed class ProviderFixture
        : IProviderFixture
    {
        private readonly IMappedArgumentExistenceRecorderFactoryProvider Sut;

        private readonly Mock<IBoolDelegateMappedArgumentExistenceRecorderFactory> BoolDelegateMock;
        private readonly Mock<IVoidDelegateMappedArgumentExistenceRecorderFactory> VoidDelegateMock;

        public ProviderFixture(
            IMappedArgumentExistenceRecorderFactoryProvider sut,
            Mock<IBoolDelegateMappedArgumentExistenceRecorderFactory> boolDelegateMock,
            Mock<IVoidDelegateMappedArgumentExistenceRecorderFactory> voidDelegateMock)
        {
            Sut = sut;

            BoolDelegateMock = boolDelegateMock;
            VoidDelegateMock = voidDelegateMock;
        }

        IMappedArgumentExistenceRecorderFactoryProvider IProviderFixture.Sut => Sut;

        Mock<IBoolDelegateMappedArgumentExistenceRecorderFactory> IProviderFixture.BoolDelegateMock => BoolDelegateMock;
        Mock<IVoidDelegateMappedArgumentExistenceRecorderFactory> IProviderFixture.VoidDelegateMock => VoidDelegateMock;
    }
}
