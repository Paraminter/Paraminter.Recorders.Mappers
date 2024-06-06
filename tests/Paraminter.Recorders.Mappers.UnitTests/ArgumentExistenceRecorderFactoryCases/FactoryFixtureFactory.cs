namespace Paraminter.Mappers.ArgumentExistenceRecorderFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        ArgumentExistenceRecorderFactory sut = new();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture
        : IFactoryFixture
    {
        private readonly IArgumentExistenceRecorderFactory Sut;

        public FactoryFixture(
            IArgumentExistenceRecorderFactory sut)
        {
            Sut = sut;
        }

        IArgumentExistenceRecorderFactory IFactoryFixture.Sut => Sut;
    }
}
