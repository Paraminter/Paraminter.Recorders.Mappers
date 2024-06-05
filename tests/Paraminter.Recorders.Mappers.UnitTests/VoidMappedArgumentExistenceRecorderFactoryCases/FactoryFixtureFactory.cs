namespace Paraminter.Mappers.VoidDelegateMappedArgumentExistenceRecorderFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        VoidDelegateMappedArgumentExistenceRecorderFactory sut = new();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture
        : IFactoryFixture
    {
        private readonly IVoidDelegateMappedArgumentExistenceRecorderFactory Sut;

        public FactoryFixture(
            IVoidDelegateMappedArgumentExistenceRecorderFactory sut)
        {
            Sut = sut;
        }

        IVoidDelegateMappedArgumentExistenceRecorderFactory IFactoryFixture.Sut => Sut;
    }
}
