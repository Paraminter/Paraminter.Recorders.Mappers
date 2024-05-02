namespace Paraminter.Mappers.VoidDelegateMappedArgumentDataRecorderFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        VoidDelegateMappedArgumentDataRecorderFactory sut = new();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture : IFactoryFixture
    {
        private readonly IVoidDelegateMappedArgumentDataRecorderFactory Sut;

        public FactoryFixture(IVoidDelegateMappedArgumentDataRecorderFactory sut)
        {
            Sut = sut;
        }

        IVoidDelegateMappedArgumentDataRecorderFactory IFactoryFixture.Sut => Sut;
    }
}
