namespace Paraminter.Mappers.BoolDelegateMappedArgumentDataRecorderFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        BoolDelegateMappedArgumentDataRecorderFactory sut = new();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture
        : IFactoryFixture
    {
        private readonly IBoolDelegateMappedArgumentDataRecorderFactory Sut;

        public FactoryFixture(
            IBoolDelegateMappedArgumentDataRecorderFactory sut)
        {
            Sut = sut;
        }

        IBoolDelegateMappedArgumentDataRecorderFactory IFactoryFixture.Sut => Sut;
    }
}
