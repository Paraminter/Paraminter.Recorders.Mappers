namespace Paraminter.Mappers.ArgumentDataRecorderFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        ArgumentDataRecorderFactory sut = new();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture
        : IFactoryFixture
    {
        private readonly IArgumentDataRecorderFactory Sut;

        public FactoryFixture(
            IArgumentDataRecorderFactory sut)
        {
            Sut = sut;
        }

        IArgumentDataRecorderFactory IFactoryFixture.Sut => Sut;
    }
}
