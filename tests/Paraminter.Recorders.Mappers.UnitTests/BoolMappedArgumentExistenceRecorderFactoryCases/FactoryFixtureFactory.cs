namespace Paraminter.Recorders.Mappers.BoolDelegateMappedArgumentExistenceRecorderFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        BoolDelegateMappedArgumentExistenceRecorderFactory sut = new();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture
        : IFactoryFixture
    {
        private readonly IBoolDelegateMappedArgumentExistenceRecorderFactory Sut;

        public FactoryFixture(
            IBoolDelegateMappedArgumentExistenceRecorderFactory sut)
        {
            Sut = sut;
        }

        IBoolDelegateMappedArgumentExistenceRecorderFactory IFactoryFixture.Sut => Sut;
    }
}
