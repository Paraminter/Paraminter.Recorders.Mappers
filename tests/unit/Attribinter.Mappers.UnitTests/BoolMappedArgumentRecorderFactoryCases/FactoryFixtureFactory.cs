namespace Attribinter.Mappers.BoolDelegateMappedArgumentRecorderFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        BoolDelegateMappedArgumentRecorderFactory sut = new();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture : IFactoryFixture
    {
        private readonly IBoolDelegateMappedArgumentRecorderFactory Sut;

        public FactoryFixture(IBoolDelegateMappedArgumentRecorderFactory sut)
        {
            Sut = sut;
        }

        IBoolDelegateMappedArgumentRecorderFactory IFactoryFixture.Sut => Sut;
    }
}
